using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using StackQuestion.Models;
using StackQuestion.Repositories;
using StackQuestion.ViewModels.Question;
using Microsoft.AspNet.Authorization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace StackQuestion.Controllers
{
    public class QuestionController : Controller
    {
        private readonly Repository<Question> _questionRepository;
        private readonly Repository<Tag> _tagRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Repository<Answer> _answerRepository;

        public QuestionController
            (
            Repository<Question> questionRepository, 
            Repository<Tag> tagRepository,
            Repository<Answer> answerRepository,
            UserManager<ApplicationUser> userManager
            )
        {
            _questionRepository = questionRepository;
            _tagRepository = tagRepository;
            _answerRepository = answerRepository;
            _userManager = userManager;
        }

        public IActionResult List(int page = 1, string tag = null, int size = 1)
        {
            var model = new QuestionViewModel
            {
                Questions = _questionRepository.GetAll
                .Where(q => q.QuestionTags.Any(qt => tag != null ? qt.Tag.Title == tag : true))
                .Skip((page - 1) * size)
                .Take(size),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = size,
                    Items = _questionRepository.GetAll.Where(q => q.QuestionTags.Any(qt => tag != null ? qt.Tag.Title == tag : true)).Count()
                }
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var model = new DetailQuestionViewModel { Question = _questionRepository.GetById(id) };
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CreateQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question() { Subject = model.Subject, Text = model.Question, PublisherDate = DateTime.Now};
                question.Author = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                question = await _questionRepository.Create(question);
                foreach(var title in SplitLine(model.Tags))
                {
                    var tag = await _tagRepository.Create(new Tag { Title = title });
                    var questionTag = new QuestionTag { QuestionId = question.Id, Question = question, TagId = tag.Id, Tag = tag };
                    question.QuestionTags.Add(questionTag);
                    tag.QuestionTags.Add(questionTag);
                    await _questionRepository.Update(question);
                    await _tagRepository.Update(tag);
                }
                return RedirectToAction(nameof(QuestionController.List), null);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AutocompleteTagSearch(string term = "")
        {
            var models = _tagRepository.GetAll
                .Where(t => t.Title.Contains(term != null ? term : ""))
                .Select(t => t.Title)
                .Distinct();

            return Json(models);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAnswer(string answer, int id)
        {
            if (ModelState.IsValid)
            {
                var question = _questionRepository.GetById(id);
                var comment = new Answer { Message = answer, PublishDate = DateTime.Now };
                comment.Author = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                comment.Question = question;
                comment = await _answerRepository.Create(comment);
                return RedirectToAction(nameof(QuestionController.Detail), new { id = id });
            }
            return View(answer);
        }

        private List<string> SplitLine(string line)
        {
            var pattern = @", ";
            var result = Regex.Split(line, pattern).Where(w => !string.IsNullOrEmpty(w)).Distinct().ToList();
            return result;
        }

        [HttpGet]
        public IActionResult EditQuestion(int id)
        {
            var model = _questionRepository.GetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditQuestion(Question model)
        {
            if (ModelState.IsValid)
            {
                var question = _questionRepository.GetById(model.Id);
                question.Subject = model.Subject;
                question.Text = model.Text;
                question = await _questionRepository.Update(question);
                return RedirectToAction(nameof(QuestionController.Detail), new { id = model.Id });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAnswer(int id, Answer model)
        {
            if (ModelState.IsValid)
            {
                var answer = _answerRepository.GetById(model.Id);
                answer.Message = model.Message;
                answer = await _answerRepository.Update(answer);
                return RedirectToAction(nameof(QuestionController.Detail), new { id = id });
            }
            return View();
        }


    }
}

using Microsoft.Data.Entity;
using StackQuestion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackQuestion.Repositories
{
    public class QuestionRepository : Repository<Question>
    {
        private StackQuestionContext context = new StackQuestionContext();

        public IEnumerable<Question> GetAll => 
            context.Questions
            .Include(q => q.Answers)
            .Include(q => q.Author)
            .Include(q => q.QuestionTags)
            .ThenInclude(qt => qt.Tag);

        public Question GetById(int id) => 
            context.Questions
            .Include(q => q.Answers)
            .Include(q => q.Author)
            .Include(q => q.QuestionTags)
            .ThenInclude(qt => qt.Tag)
            .SingleOrDefault(q => q.Id == id);

        public async Task<Question> Create(Question question)
        {
            question = context.Questions.Add(question).Entity;
            await context.SaveChangesAsync();
            return question;
        }

        #region dispose

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<Question> Update(Question item)
        {
            var current = GetById(item.Id);
            current.Subject = item.Subject;
            current.Text = item.Text;
            current.QuestionTags = item.QuestionTags;
            await context.SaveChangesAsync();
            return current;
        }

        #endregion
    }
}

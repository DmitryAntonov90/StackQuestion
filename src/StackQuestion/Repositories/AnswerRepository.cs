using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackQuestion.Models;
using System.Linq;

namespace StackQuestion.Repositories
{
    public class AnswerRepository : Repository<Answer>
    {
        private StackQuestionContext context = new StackQuestionContext();

        public IEnumerable<Answer> GetAll => context.Answer;

        public Answer GetById(int id) => context.Answer.FirstOrDefault(c => c.Id == id);

        public async Task<Answer> Create(Answer item)
        {
            item.PublishDate = DateTime.Now;
            item = context.Answer.Add(item).Entity;
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<Answer> Update(Answer item)
        {
            var current = GetById(item.Id);
            current.Message = item.Message;
            current.UpdateDate = DateTime.Now;
            await context.SaveChangesAsync();
            return current;
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

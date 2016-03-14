using Microsoft.Data.Entity;
using StackQuestion.Models;
using StackQuestion.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackQuestion.Repositories
{
    public class TagRepository : Repository<Tag>
    {
        private StackQuestionContext context = new StackQuestionContext();

        public IEnumerable<Tag> GetAll => context.Tags.Include(t => t.QuestionTags).ThenInclude(qt => qt.Question);

        public Tag GetById(int id) => context.Tags.Include(t => t.QuestionTags).ThenInclude(qt => qt.Question)
            .Where(t => t.Id == id).FirstOrDefault();

        public async Task<Tag> Create(Tag item)
        {
            var tag = context.Tags.Where(t => t.Title == item.Title).FirstOrDefault();
            if (tag == null)
            {
                tag = context.Tags.Add(item).Entity;
                await context.SaveChangesAsync();
            }
            return tag;
        }

        public async Task<Tag> Update(Tag item)
        {
            var current = GetById(item.Id);
            current.Title = item.Title;
            current.QuestionTags = item.QuestionTags;
            await context.SaveChangesAsync();
            return current;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

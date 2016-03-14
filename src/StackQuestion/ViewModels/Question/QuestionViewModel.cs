using System;
using System.Collections.Generic;

namespace StackQuestion.ViewModels.Question
{
    public class QuestionViewModel
    {
        public IEnumerable<Models.Question> Questions { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}

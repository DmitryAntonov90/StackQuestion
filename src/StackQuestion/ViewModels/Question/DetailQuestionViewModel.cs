using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StackQuestion.ViewModels.Question
{
    public class DetailQuestionViewModel
    {
        public Models.Question Question { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}

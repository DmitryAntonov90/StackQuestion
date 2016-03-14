using StackQuestion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StackQuestion.ViewModels.Question
{
    public class CreateQuestionViewModel
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Tags { get; set; }

    }
}

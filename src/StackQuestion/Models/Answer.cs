using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackQuestion.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        [Required]
        public Question Question { get; set; }

        public int QuestionId { get; set; }
    }
}

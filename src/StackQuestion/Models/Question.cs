using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackQuestion.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime PublisherDate { get; set; }

        [DefaultValue(0)]
        public int Votes { get; set; }

        [DefaultValue(0)]
        public int Views { get; set; }

        public List<QuestionTag> QuestionTags { get; set; } = new List<QuestionTag>();

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        public List<Answer> Answers { get; set; } = new List<Answer>();

        [NotMapped]
        public string Status
        {
            get
            {
                var now = DateTime.Now;
                var diff = now - PublisherDate;
                if (diff.TotalDays < 1.0)
                {
                    if (diff.TotalSeconds < 1.0)
                        return string.Format($"Asked {(int)diff.TotalSeconds } secs ago");
                    else if (diff.TotalMinutes < 60.0)
                        return string.Format($"Asked {(int)diff.TotalMinutes} minute ago");
                    else if (diff.TotalHours < 24.0)
                        return string.Format($"Asked {(int)diff.TotalHours} hour ago");
                    else
                        return "error";
                }
                else if (diff.TotalDays <= 30.0)
                    return string.Format($"Asked {(int)diff.TotalDays} days ago");
                else if (diff.Days < 365)
                    return string.Format($"Asked {(int)365.0 / diff.Days}");
                else
                    return string.Format("Asked more then year ago");
            }
        }
    }
}

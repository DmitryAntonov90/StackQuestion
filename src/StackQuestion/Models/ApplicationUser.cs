using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StackQuestion.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Question> questions { get; set; } = new List<Question>();

        public List<Answer> Answer { get; set; } = new List<Answer>();
    }
}

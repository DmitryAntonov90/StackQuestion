using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackQuestion.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [RegularExpression("^([a-z]+[a-z#+.-]+[a-z+#]+)$", ErrorMessage = "Tag name does't have space and end on symbol a-z + #)]")]
        public string Title { get; set; }

        public List<QuestionTag> QuestionTags { get; set; } = new List<QuestionTag>();
    }
}

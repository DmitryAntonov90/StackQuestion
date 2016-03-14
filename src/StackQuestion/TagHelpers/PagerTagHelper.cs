using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.TagHelpers;
using Microsoft.Extensions.WebEncoders;
using StackQuestion.Controllers;
using System.IO;
using System.Text;

namespace StackQuestion.TagHelpers
{
    public class PagerTagHelper : TagHelper
    {
        private IUrlHelper _url;

        public PagerTagHelper(IUrlHelper url)
        {
            _url = url;
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            var result = new StringBuilder();
            if (CurrentPage > 1)
            {
                var prev = new TagBuilder("a");
                prev.MergeAttribute("href", _url.Action(nameof(QuestionController.List), new { page = CurrentPage - 1 }));
                prev.InnerHtml.AppendHtml("prev");
                prev.AddCssClass("btn btn-navigation");
                var writer = new StringWriter();
                prev.WriteTo(writer, new HtmlEncoder());
                result.AppendLine(writer.ToString());
            }
            if (TotalPages >= 6 && CurrentPage - 2 > 1)
            {
                var a = new TagBuilder("a");
                a.MergeAttribute("href", _url.Action(nameof(QuestionController.List), new { page = 1 }));
                a.InnerHtml.AppendHtml(1.ToString());
                if (CurrentPage == 1)
                    a.AddCssClass("active");
                a.AddCssClass("btn btn-navigation");
                if (CurrentPage  - 3 > 1)
                    a.AddCssClass("page-first");
                var writer = new StringWriter();
                a.WriteTo(writer, new HtmlEncoder());
                result.AppendLine(writer.ToString());
                var i = CurrentPage - 2;
                for (; i <= CurrentPage + 3 && i < TotalPages; i++)
                {
                    a = new TagBuilder("a");
                    a.MergeAttribute("href", _url.Action(nameof(QuestionController.List), new { page = i }));
                    a.InnerHtml.AppendHtml(i.ToString());
                    if (CurrentPage == i)
                        a.AddCssClass("active");
                    a.AddCssClass("btn btn-navigation");
                    writer = new StringWriter();
                    a.WriteTo(writer, new HtmlEncoder());
                    result.AppendLine(writer.ToString());
                }
                a = new TagBuilder("a");
                a.MergeAttribute("href", _url.Action(nameof(QuestionController.List), new { page = TotalPages }));
                a.InnerHtml.AppendHtml(TotalPages.ToString());
                if (CurrentPage == TotalPages)
                    a.AddCssClass("active");
                a.AddCssClass("btn btn-navigation");
                if (CurrentPage + 2 < TotalPages)
                    a.AddCssClass("page-last");
                writer = new StringWriter();
                a.WriteTo(writer, new HtmlEncoder());
                result.AppendLine(writer.ToString());
            }
            else
            {
                for (int i = 1; i <= 5; i++)
                {
                    var a = new TagBuilder("a");
                    a.MergeAttribute("href", _url.Action(nameof(QuestionController.List), new { page = i }));
                    a.InnerHtml.AppendHtml(i.ToString());
                    if (CurrentPage == i)
                        a.AddCssClass("active");
                    a.AddCssClass("btn btn-navigation");
                    var writer = new StringWriter();
                    a.WriteTo(writer, new HtmlEncoder());
                    result.AppendLine(writer.ToString());
                }
            }

            if (CurrentPage < TotalPages)
            {
                var next = new TagBuilder("a");
                next.MergeAttribute("href", _url.Action(nameof(QuestionController.List), new { page = CurrentPage + 1 }));
                next.InnerHtml.AppendHtml("next");
                next.AddCssClass("btn btn-navigation");
                var writer = new StringWriter();
                next.WriteTo(writer, new HtmlEncoder());
                result.AppendLine(writer.ToString());
            }
            output.Content.SetHtmlContent(result.ToString());
            output.Attributes.Clear();
        }
    }
}

using StackQuestion.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackQuestion.ViewModels.Question
{
    public class PageInfo
    {
        public int Items { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)Items / ItemsPerPage);
    }
}

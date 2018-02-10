using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kingston.Demo.Ver1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kingston.Demo.Ver1.ViewModels
{
    public class IssueViewModel
    {
        public IssueViewModel()
        {
            files = new List<IFormFile>();
            IssueSubDatas = new List<IssueSub>();

        }
        public Issue IssueData { get; set; }
        public IEnumerable<Issue> IssueDatas { get; set; }
        public List<SelectListItem> TblCode_Sys { get; set; }
        public List<IFormFile> files { get; set; }
        public IEnumerable<IssueFiles> IssueFiles { get; set; }
        public IssueSub IssueSubData { get; set; }
        public List<IssueSub> IssueSubDatas { get; set; }
    }
}

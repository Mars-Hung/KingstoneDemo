using System;
using System.Collections.Generic;

namespace Kingston.Demo.Ver1.Models
{
    public partial class IssueFiles
    {
        public int Id { get; set; }
        public int? IssueId { get; set; }
        public string FileName { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}

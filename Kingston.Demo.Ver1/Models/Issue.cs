using System;
using System.Collections.Generic;

namespace Kingston.Demo.Ver1.Models
{
    public partial class Issue
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Desc { get; set; }
        public int? Status { get; set; }
        public string Tags { get; set; }
        public string KeyInUser { get; set; }
        public DateTime? KeyInDate { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kingston.Demo.Ver1.Models
{
    public partial class IssueSub
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Desc { get; set; }
        public DateTime? KeyInDate { get; set; }
        public string KeyInUser { get; set; }
    }
}

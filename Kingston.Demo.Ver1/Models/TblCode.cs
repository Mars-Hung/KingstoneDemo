using System;
using System.Collections.Generic;

namespace Kingston.Demo.Ver1.Models
{
    public partial class TblCode
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? Seq { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUser { get; set; }
    }
}

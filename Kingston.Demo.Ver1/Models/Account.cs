using System;
using System.Collections.Generic;

namespace Kingston.Demo.Ver1.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime? KeyInDate { get; set; }
    }
}

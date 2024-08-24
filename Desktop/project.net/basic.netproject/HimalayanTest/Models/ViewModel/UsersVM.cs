    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HimalayanTest.Models.ViewModel
{
    public class UsersVM
    {
        public int UsersID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserGroupID { get; set; }
        public string Fullname { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int Status { get; set; }

        public string GroupName { get; set; }
        public string GroupCode { get; set; }
    }
}

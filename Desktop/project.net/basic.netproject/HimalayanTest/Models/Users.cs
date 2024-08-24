using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HimalayanTest.Models
{
    public class Users
    {
        [Key]
        public int UsersID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserGroupID { get; set; }
        public string Fullname { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int Status { get; set; }



        [ForeignKey("UserGroupID")]
        public virtual UserGroup UserGroup { get; set; }
    }
}

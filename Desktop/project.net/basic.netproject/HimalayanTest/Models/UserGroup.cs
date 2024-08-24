using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HimalayanTest.Models
{
    public class UserGroup
    {
        [Key]
        public int UserGroupID { get; set; }
        public string UserGroupName { get; set; }
        public string UserGroupCode { get; set; }
        public bool IsActive { get; set; }
    }
}

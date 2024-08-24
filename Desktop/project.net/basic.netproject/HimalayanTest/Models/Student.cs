using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HimalayanTest.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
    }
}

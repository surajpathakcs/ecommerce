using HimalayanTest.DAO;
using HimalayanTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HimalayanTest.Controllers
{
    public class StudentController : Controller
    {
        ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult ListAll()
        {
            return View();
        }
    }
}

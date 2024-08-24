using HimalayanTest.DAO;
using HimalayanTest.Models;
using HimalayanTest.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HimalayanTest.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(Users ug)
        {
            List<Users> groups = _context.Users.Where(x => x.Status == 1).ToList();
            return View(groups);
        }
        public IActionResult Test()
        {
            return View();
        }

        public JsonResult Create(string groupname, string groupfullname, string grouppass, string groupconfirm, int id)
        {
            if (grouppass != groupconfirm)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Passwords do not match"
                });
            }

            if (id == 0)
            {
                //create new part
                var oldGroup = _context.Users.Where(x => x.Username == groupname).FirstOrDefault();

                if (oldGroup == null)
                {
                    Users ug = new Users()
                    {
                        Username = groupname,
                        Fullname = groupfullname,
                        Password = groupconfirm,
                        Status = 1,
                        UserGroupID = 9
                    };

                    _context.Users.Add(ug);
                    _context.SaveChanges();


                    return Json(new
                    {
                        Success = true,
                        Message = "User Added Successfully!"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Username repeated"
                    });
                }
            }
            else
            {
                var dupUserName = _context.Users.Where(x => x.Username == groupname 
                                                        && x.UsersID != id)
                    .FirstOrDefault();


                if (dupUserName != null)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Username repeated"
                    });
                }
                // update part 
                Users ug = _context.Users
                           .Where(x => x.UsersID == id)
                           .FirstOrDefault();
                if (ug == null)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "User Information Not Found For Edit"
                    });
                }
                else
                {
                    ug.Username = groupname;
                    ug.Password = groupconfirm;
                    ug.Fullname = groupfullname;

                    _context.SaveChanges();

                    return Json(new
                    {
                        Success = true,
                        Message = "User Information Updated Successfully"
                    });
                }
            }

        }

        public JsonResult Delete(int id)
        {
            Users ug = _context.Users
                             .Where(x => x.UsersID == id)
                             .FirstOrDefault();
            if (ug == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "User Information Not Found For Delete"
                });
            }
            else
            {
                //_context.UserGroup.Remove(ug); // for hard delete i.e. remove row from database

                ug.Status = 0;
                _context.SaveChanges();

                return Json(new
                {
                    Success = true,
                    Message = "User Information Deleted Successfully"
                });


            }
        }

        public JsonResult GetByID(int id)
        {
            Users ug = _context.Users
                                .Where(x => x.UsersID == id)
                                .FirstOrDefault();
            if (ug == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "User Information Not Found"
                });
            }
            else
            {
                return Json(new
                {
                    Success = true,
                    Data = ug
                });
            }
        }
    }
    /*public IActionResult Index()
    {
      //  List<UsersVM> usr = _context.Users
      //      .Where(x => x.UserGroupID == 1)
      //      .Select(s => new UsersVM
      //      {
      //          Username = s.Username,
      //          Fullname = s.Fullname,
      //          GroupName = s.UserGroup.UserGroupName,
      //          GroupCode = s.UserGroup.UserGroupCode,
      //          ValidFrom = s.ValidFrom
      //      })
      //      .OrderBy(o => o.Username)  // orders data in ascending order
      //      .OrderByDescending(o => o.Fullname)
      //      .ToList();
      //
      //  // .First() .FirstOrDefault()  .Last() .LastOrDefault()
      //
      //  Users u = _context.Users.Where(x => x.UserGroupID == 5).FirstOrDefault();
      //  // returns null by FirstOrDefault if no any matching data found
        return View();
    }*/
}


using HimalayanTest.DAO;
using HimalayanTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HimalayanTest.Controllers
{
    public class UserGroupController : Controller
    {
        ApplicationDbContext _context;
        public UserGroupController(ApplicationDbContext context)
        {
            _context = context;

        }


        public IActionResult Index(UserGroup ug)
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }



        [HttpPost]
        public JsonResult Create([FromBody] UserGroup ug)
        {
            if (ug.UserGroupID == 0)
            {
                //create new part
                var oldGroup = _context.UserGroup.Where(x => x.UserGroupCode == ug.UserGroupCode).FirstOrDefault();

                if (oldGroup == null)
                {
                    ug.IsActive = true;

                    _context.UserGroup.Add(ug);
                    _context.SaveChanges();


                    return Json(new
                    {
                        Success = true,
                        Message = "User Group Added Successfully!"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "User Group code repeated"
                    });
                }
            }
            else
            {
                // update part 
                UserGroup ugOld = _context.UserGroup
                           .Where(x => x.UserGroupID == ug.UserGroupID)
                           .FirstOrDefault();
                if (ug == null)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "User Group Information Not Found For Edit"
                    });
                }
                else
                {
                    ugOld.UserGroupName = ug.UserGroupName;
                    ugOld.UserGroupCode = ug.UserGroupCode;

                    _context.SaveChanges();

                    return Json(new
                    {
                        Success = true,
                        Message = "User Group Information Updated Successfully"
                    });
                }
            }
        }

        public JsonResult Delete(int id)
        {
            UserGroup ug = _context.UserGroup
                             .Where(x => x.UserGroupID == id)
                             .FirstOrDefault();
            if (ug == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "User Group Information Not Found For Delete"
                });
            }
            else
            {
                //_context.UserGroup.Remove(ug); // for hard delete i.e. remove row from database

                ug.IsActive = false;
                _context.SaveChanges();

                return Json(new
                {
                    Success = true,
                    Message = "User Group Information Deleted Successfully"
                });


            }
        }

        public JsonResult GetByID(int id)
        {
            UserGroup ug = _context.UserGroup
                                .Where(x => x.UserGroupID == id)
                                .FirstOrDefault();
            if (ug == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "User Group Information Not Found"
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

        public JsonResult GetActiveData(string grpName,string grpCode)
        {
            var datas = _context.UserGroup
                .Where(x => x.IsActive == true
                    && (string.IsNullOrEmpty(grpName) ||  x.UserGroupName.Contains(grpName))
                    && (string.IsNullOrEmpty(grpCode) || x.UserGroupCode.Contains(grpCode))
                )
                .ToList();
            return Json(new
            {
                Success = true,
                Data = datas
            });
        }
    }
}

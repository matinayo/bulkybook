using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class UserController : Controller
    {
        // private readonly IUnitOfWork _unitOfWork;
        // working directly with DbContext
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);

            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while locking/unlocking" });
            }

            // if value is less than today's date = account is unlocked
            // if greater than today's date, it is locked
            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                // user is currently locked, unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                // user is not locked, lock them
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            _db.SaveChanges();
            return Json(new { success = true, message = "Operation Successful." });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {

            // get user details
            var userList = _db.ApplicationUser.Include(u => u.Company).ToList();  // from ApplicationUser model, if CompanyId is populated, it would load the Company object

            // display role of user
            var userRole = _db.UserRoles.ToList();  // userRole is a mapping of userList and their roles
            var roles = _db.Roles.ToList();

            foreach(var user in userList)
            {
                // populate role in ApplicationUser model inorder to be displayed in datatable
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

                if(user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = "",
                        
                    };
                }
            }

            return Json(new { data = userList });
        }

        #endregion
    }
}

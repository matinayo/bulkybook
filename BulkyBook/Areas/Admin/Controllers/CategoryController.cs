using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        // called from js to navigate to edit page
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();

            if(id == null)
            {
                // for creating
                return View(category);
            }
            // for edit
            category = _unitOfWork.Category.Get(id.GetValueOrDefault());

            if(category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // called when a form is submitted
        [HttpPost]
        [ValidateAntiForgeryToken] // prevents submitting from another website using a user credential with hidden contents
        public IActionResult Upsert(Category category){

            if (ModelState.IsValid)
            {
                if(category.Id == 0)
                {
                    // create new category
                    _unitOfWork.Category.Add(category);
                }
                else
                {
                    // update
                    _unitOfWork.Category.Update(category);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {
            // retrieve all categories and return in json format
            var allObj = _unitOfWork.Category.GetAll();

            return Json(new { data = allObj });
        }

        // add api call for delete
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Category.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Success" });
        }

        #endregion
    }
}

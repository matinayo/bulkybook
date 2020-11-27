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
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new Company();

            if(id == null)
            {
                return View(company);
            }

            company = _unitOfWork.Company.Get(id.GetValueOrDefault());

            if(company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // prevents submitting from another website using a user credential with hidden contents
        public IActionResult Upsert(Company company)
        {

            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    // create new category
                    _unitOfWork.Company.Add(company);
                }
                else
                {
                    // update
                    _unitOfWork.Company.Update(company);
                }

                _unitOfWork.Save();
                // redirect to Index page
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(){
            var objFromDb = _unitOfWork.Company.GetAll();

            return Json(new { data = objFromDb });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Company.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.Company.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfully" });
                
        }

        #endregion

    }
}

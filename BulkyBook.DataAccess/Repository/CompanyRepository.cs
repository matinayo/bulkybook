using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company company)
        {
            var objFromDb = _db.Company.FirstOrDefault(s => s.Id == company.Id);

            if(objFromDb != null)
            {
                objFromDb.Name = company.Name;
                objFromDb.StreetAddress = company.StreetAddress;
                objFromDb.City = company.City;
                objFromDb.State = company.State;
                objFromDb.PostalCode = company.PostalCode;
                objFromDb.PhoneNumber = company.PhoneNumber;
                objFromDb.IsAuthorizedCompany = company.IsAuthorizedCompany;
            }
        }
    }
}

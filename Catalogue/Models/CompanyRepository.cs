using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Catalogue.Models
{
    public class CompanyRepository: ICompanyRepository
    {
        private CatalogueContext _db { get; set; }

        public CompanyRepository()
            : this(new CatalogueContext())
        {

        }

        public CompanyRepository(CatalogueContext db)
        {
            _db = db;
        }

        public Company Get(int id)
        {
            return _db.Companies.SingleOrDefault(c => c.Id == id);
        }

        public IQueryable<Company> GetAll()
        {
            return _db.Companies;
        }

        public Company Add(Company Company)
        {
            _db.Companies.Add(Company);
            _db.SaveChanges();
            return Company;
        }

        public Company Update(Company Company)
        {
            _db.Entry(Company).State = EntityState.Modified;
            _db.SaveChanges();
            return Company;
        }

        public void Delete(int CompanyId)
        {
            var Company = _db.Companies.Single(c => c.Id == CompanyId);
            _db.Companies.Remove(Company);
            _db.SaveChanges();
        }
    }
}
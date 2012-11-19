using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalogue.Models;
namespace Catalogue.Tests
{
    public class SampleDataRepository : ICompanyRepository
    {
        private IList<Company> companies;
        public SampleDataRepository()
        {
            companies = new List<Company>(SampleData.sampleCompanies);
        }

        public Company Get(int id)
        {
            return companies.SingleOrDefault(c => c.Id == id);
        }

        public IQueryable<Company> GetAll()
        {
            return companies.AsQueryable();
        }

        public Company Add(Company company)
        {
            companies.Add(company);
            return company;
        }

        public Company Update(Company updatedCompany)
        {
            var company = this.Get(updatedCompany.Id);
            company.Name = updatedCompany.Name;
            company.PIN = updatedCompany.PIN;
            company.Contact = updatedCompany.Contact;
            company.Description = updatedCompany.Description;
            return updatedCompany;
        }

        public void Delete(int id)
        {
            var company = this.Get(id);
            companies.Remove(company);
        }
    }
}

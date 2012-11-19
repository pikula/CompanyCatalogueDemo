using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogue.Models
{
    public interface ICompanyRepository
    {
        Company Get(int id);
        IQueryable<Company> GetAll();
        Company Add(Company company);
        Company Update(Company company);
        void Delete(int id);        
    }
}
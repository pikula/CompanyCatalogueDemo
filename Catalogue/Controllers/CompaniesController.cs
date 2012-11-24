using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Catalogue.Models;

namespace Catalogue.Controllers
{
    public class CompaniesController : ApiController
    {
        private ICompanyRepository _repository { get; set; }

        public CompaniesController(ICompanyRepository repository)
        {
            _repository = repository;
        }

        // GET api/Companies
        [Queryable]
        public IQueryable<Company> GetCompanies()
        {

            return _repository.GetAll().AsQueryable();
        }

        // GET api/Companies/5
        public Company GetCompany(int id)
        {
            Company company = _repository.Get(id);
            if (company == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return company;
        }

        // PUT api/Companies/5
        public HttpResponseMessage PutCompany(int id, Company company)
        {
            if (ModelState.IsValid && id == company.Id)
            {

                try
                {
                    _repository.Update(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Companies
        public HttpResponseMessage PostCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(company);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, company);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/companies/" + company.Id.ToString());
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Companies/5
        public Company DeleteCompany(int id)
        {
            Company company = _repository.Get(id);
            if (company == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            try
            {
                _repository.Delete(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return company;
        }
    }
}
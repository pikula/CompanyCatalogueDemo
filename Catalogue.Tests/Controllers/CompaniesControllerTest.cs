using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catalogue.Controllers;
using System.Linq;
using Catalogue.Models;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Net;

namespace Catalogue.Tests.Controllers
{
    [TestClass]
    public class CompaniesControllerTest
    {
        [TestMethod]
        public void GetCompanies()
        {
            var controller = new CompaniesController(new SampleDataRepository());
            IQueryable<Company> companies = controller.GetCompanies();
            Assert.IsTrue(companies.Count() > 0);
        }

        [TestMethod]
        public void Post()
        {
            Company company = new Company { Id = 5, Name = "Testing1", PIN = "92727475465" };
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            var repository = new SampleDataRepository();
            var controller = new CompaniesController(repository) { Request = request };
            HttpResponseMessage response = controller.PostCompany(company);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Headers.Location);
            Company newCompany = response.Content.ReadAsAsync<Company>().Result;
            Assert.IsNotNull(newCompany);
        }

        [TestMethod]
        public void GetCompany()
        {
            var controller = new CompaniesController(new SampleDataRepository());
            int id = 1;
            Company company = controller.GetCompany(id);
            Assert.IsNotNull(company);
            Assert.AreEqual(id, company.Id);
        }

        [TestMethod]
        public void Delete()
        {
            var repository = new SampleDataRepository();
            var controller = new CompaniesController(repository);
            int id = 1;
            controller.DeleteCompany(id);
            Assert.IsNull(repository.Get(id));
        }
    }
}

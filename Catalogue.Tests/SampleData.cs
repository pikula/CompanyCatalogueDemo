using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Catalogue.Models;
namespace Catalogue.Tests
{
    public static class SampleData
    {
        public static Company[] sampleCompanies = new Company[]
            {
                new Company{Id = 1, Name = "LADIDA-IND", PIN = "97941282318", Contact = "tel:01/1234567", Description = "Zagreb"},
                new Company{Id = 2, Name = "Adrialis d.o.o", PIN = "62419888291", Contact = "tel:01/1111111", Description = "Zagreb"},
                new Company{Id = 3, Name = "Dest d.o.o", PIN = "18393644700", Contact = "tel:051/2222222"},
                new Company{Id = 4, Name = "Correre", PIN = "18393644700", Contact = "tel:031/333333, mail:correre@testing.hr", Description = "Sportska oprema"}
            };

        public static Company[] Companies
        {
            get
            {
                return sampleCompanies;
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Catalogue.Models
{
    public class Company
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string PIN { get; set; }
        public string Contact { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
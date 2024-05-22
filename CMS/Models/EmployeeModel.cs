using CMS.Controllers;
using CMS.Views.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace CMS.Models
{
    public class EmployeeModel 
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }


    }
}




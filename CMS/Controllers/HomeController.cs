using CMS.DbEntity;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                //// Create
                //var newCategory = new BlogCategory { Name = "IT", CreatedDate = DateTime.Now };
                //context.BlogCategory.Add(newCategory);
                //context.SaveChanges();

                //// Read
                //var category = context.BlogCategory.FirstOrDefault(e=>e.CategoryId==2); // Assuming 1 is the ID of the category
                //Console.WriteLine($"Category: {category.Name}, CreatedDate: {category.CreatedDate}");

                ////Update
                //category.Name = category.Name + " Updated";
                //context.SaveChanges();

                ////Delete
                //context.BlogCategory.Remove(category);
                //context.SaveChanges();
            }

            var adoNetService = new ADonet();
            var blogList = adoNetService.GetList();
            return View(blogList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles="Admin")]
        public IActionResult AdminView()
        {
            return Content("Authorized");
        }


        [Authorize(policy: "AllowBranchManager")]
        public IActionResult BranchManagerView()
        {
            return Content("Authorized");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

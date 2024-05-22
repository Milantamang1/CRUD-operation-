using CMS.Models;
using CMS.Views.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMS.Controllers
{
    public class DiController : Controller
    {
        private IEmailServices service;
        public DiController(IEmailServices services)
        {
            service = services;
        }
        //public IActionResult Index([FromServices] Icustomer customer)
        //{
        //    customer.Discount();
        //    return View();
        //}
    }
    public interface IEmailServices
    {
        void CallMe();
    }
    public class EmailService : IEmailServices
    {
        public EmailService()
        {

        }
        public void CallMe()
        {

        }
    }
    public class EmployeeController : Controller
    {
        [Route("List")]
        public IActionResult Index()
        {
            /*  var list = GetStudentList();

              //value type to object => boxing
              //object to value type => unboxing

              object studentObj = _cache.Get("StudentList");
             var studentList = (List<EmployeeModel>)studentObj;
              if (studentList != null && studentList.Any())
              {
                  list = studentList;
              }
              else
              {
                 // student.StudentList = student.GetStudentList();
                  _cache.Set("StudentList", list);
              }
              ViewBag.Message = "Added successfully";
              return View(list);
          }*/

            //   public IActionResult Details()
            //    {
            //        return View();
            //    }
            //    public IActionResult EmployeeDetail()
            //    {
            //        var model = new EmployeeModel
            //        {
            //            Name = "Naship Kumar",
            //            Address = "Bardiya"
            //        };
            //        return View(model);

            //    }
            ADonet obj1 = new ADonet();
            var list = obj1.GetList();
            return View(list);
        }
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _cache;
        public EmployeeController(ILogger<HomeController> logger,
            IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }
        public List<EmployeeModel> GetStudentList()
        {
            var list = new List<EmployeeModel>();
            var student1 = new EmployeeModel
            {
                id = 1,
                Name = "Gaurav",
                Address = "KTM",
                Age = 23
            };
            list.Add(student1);

            var student2 = new EmployeeModel
            {
                id = 2,
                Name = "Sandip",
                Address = "PKR",
                Age = 22
            };
            list.Add(student2);
            var student3 = new EmployeeModel
            {
                id = 3,
                Name = "Mandip",
                Address = "BRT",
                Age = 24
            };
            list.Add(student3);

            return list;

        }
        public IActionResult Create()//Resource(controller,action)
        {
            /*var model = new Student();

            var cDrop = new CountryDropdown();
            var list = cDrop.GetList();
            TempData["CountryList"] = JsonConvert.SerializeObject(list);

            return View(model);*/
            var list = new EmployeeModel();
            ViewBag.message = "Added Succesfully";
            return View(list);

        }

        [HttpPost]
        public IActionResult Create(EmployeeModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            //object studentObj = _cache.Get("StudentList");
            //var studentList = (List<EmployeeModel>)studentObj;
            //studentList.Add(data);
            //   _cache.Set("StudentList", studentList);

            //_cache.Set("StudentList", studentList);
            ADonet obj1 = new ADonet();
            obj1.Insert(data.Name, data.Age);

            // return View(list);


            return RedirectToAction("Index");


            //    return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //var student = new EmployeeModel();
            //var studentList = GetStudentList();
            //var model = studentList.Where(x => x.id == id).FirstOrDefault();
            ADonet obj1 = new ADonet();
            var model = obj1.GetById(id);
            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            //object studentObj = _cache.Get("StudentList");
            //var studentList = (List<EmployeeModel>)studentObj;

            //var entity = studentList.Where(x => x.id == id).FirstOrDefault();
            //studentList.Remove(entity);
            //_cache.Set("StudentList", studentList);
            //return RedirectToAction("Index");
            ADonet obj1 = new ADonet();
            obj1.Delete(id);
            return RedirectToAction("Index");
            return View();
        }


        [HttpPost]
        public IActionResult Edit(EmployeeModel data)
        {
            //object studentObj = _cache.Get("StudentList");
            //var studentList = (List<EmployeeModel>)studentObj;

            //foreach (var item in studentList)
            //{
            //    if (item.id == data.id)
            //    {
            //        item.Name = data.Name;
            //        item.Email = data.Email;
            //        item.Address = data.Address;
            //       item.Age = data.Age;
            //   }
            //}
            //_cache.Set("StudentList", studentList);
            ADonet obj1 = new ADonet();
            obj1.Edit(data.id, data.Name, data.Age);
            return RedirectToAction("Index");
            //  ADonet obj1 = new ADonet();
            // obj1(data.Name, data.Age);

        }


    }
}

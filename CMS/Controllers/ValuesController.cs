using CMS.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            var list = GetStudentList();
            return Ok(list);
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

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

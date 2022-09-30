using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;

namespace DemoProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static List<Student> list = new List<Student>() {
             new Student(){ ID = "003", StudentName = "学生3", StudentAge = 17 }
         };

        [HttpPost]
        public List<Student> aa()
        {
            return list;
        }

        [HttpGet]
        public List<Student> GetList()
        {
            return list;
        }

        [HttpGet]
        public Student GetModel(string id)
        {
            return list.Find(t => t.ID == id);
        }
    }

    public class Student
    {
        public string ID { get; set; }
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
    }
}
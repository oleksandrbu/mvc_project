using System;
using Microsoft.AspNetCore.Mvc;

namespace mvc_project{
    [Route("api/tasks")]
    public class ApiController : Controller{
        private static TaskService dbTasks;

        static ApiController(){
            dbTasks = new TaskService();
        }

        [HttpGet("")]
        public IActionResult AllTasks(){
            return Json(dbTasks.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult TaskById(int id){
            return Json(dbTasks.GetById(id));
        }

        [HttpPost("")]
        public void AddTask(string name, bool done){
            dbTasks.Add(name, done);
        }
    }
 }
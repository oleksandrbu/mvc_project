using System;
using Microsoft.AspNetCore.Mvc;

namespace mvc_project{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase{
        private static TaskService dbTasks;

        static TaskController(){
            dbTasks = new TaskService();
        }

        [HttpGet("")]
        public IActionResult AllTasks(){
            return new JsonResult(dbTasks.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult TaskById(int id){
            return new JsonResult(dbTasks.GetById(id));
        }

        [HttpPost("")]
        public void AddTask(Task task){
            dbTasks.Add(task);
        }

        [HttpPut("{id}")]
        public IActionResult PutTask(int id, Task task){
            task.Id = id;
            
            return new JsonResult(dbTasks.Put(task));
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask(int id, Task task){
            task.Id = id;

            return new JsonResult(dbTasks.Patch(task));
        }

        [HttpDelete("{id}")]
        public void DeleteTask(int id){
            dbTasks.Delete(id);
        }
    }
 }
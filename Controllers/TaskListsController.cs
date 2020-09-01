using System;
using Microsoft.AspNetCore.Mvc;

namespace mvc_project{
    [ApiController]
    [Route("api/tasklists")]
    public class TaskListController : ControllerBase{
        private static TaskListService dbTasks;

        static TaskListController(){
            dbTasks = new TaskListService();
        }

        [HttpGet("")]
        public IActionResult AllTasks(){
            return new JsonResult(dbTasks.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult TaskById(int id){
            return new JsonResult(dbTasks.GetById(id));
        }
        [HttpGet("{id}/list")]
        public IActionResult TaskByIdList(int id){
            return new JsonResult(dbTasks.GetByIdList(id));
        }

        [HttpPost("")]
        public void AddTask(TaskList taskList){
            dbTasks.Add(taskList);
        }

        [HttpPut("{id}")]
        public IActionResult PutTask(int id, TaskList taskList){
            taskList.Id = id;
            
            return new JsonResult(dbTasks.Put(taskList));
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask(int id, TaskList taskList){
            taskList.Id = id;

            return new JsonResult(dbTasks.Patch(taskList));
        }

        [HttpDelete("{id}")]
        public void DeleteTask(int id){
            dbTasks.Delete(id);
        }
    }
 }
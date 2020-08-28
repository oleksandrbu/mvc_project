using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;

namespace asp_react.Controllers
{
    [Route("api/tasks")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        public TaskController(TaskService taskService)
        {
            this._taskService = taskService;
        }
        private string UserId
        {
            get 
            { 
                return User.Claims
                    .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                    .Select(c => c.Value)
                    .First();
            }
        }

        // GET api/tasks
        [HttpGet("")]
        public List<Task> GetTasks()
        {
            return _taskService.AllByUserId(UserId);
        }

        // GET api/tasks/5
        [HttpGet("{id}")]
        public ActionResult<Task> GetTaskById(int id)
        {
            return null;
        }

        // POST api/tasks
        [HttpPost("")]
        public Task PostTask(Task task)
        {
            task.UserId = UserId;
            return _taskService.Create(task);
        }
    }
}
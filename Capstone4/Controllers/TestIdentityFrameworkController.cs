using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone4.Data;
using Capstone4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone4.Controllers
{
    public class TestIdentityFrameworkController : Controller
    {
        private readonly TestIdentityFrameworkContext _context;
        private readonly ApplicationDbContext _identityContext;
        public TestIdentityFrameworkController(TestIdentityFrameworkContext context, ApplicationDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        public IActionResult Index()
        {

            //var userName = User.FindFirst(ClaimTypes.NameIdentifier);
            var taskList = _context.Tasks.ToList();
            return View(taskList);
        }
        public IActionResult RemoveTask(int taskID) 
        {
            _context.Tasks.Remove(_context.Tasks.Find(taskID));
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult AddNewTask() 
        {
            return View();
        }

        public IActionResult AddedNewTask(Tasks newTask)
        {
            _context.Tasks.Add(newTask);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditTask(int taskId)
        {
            return View(_context.Tasks.Find(taskId));
        }

        public IActionResult UpdateTask(Tasks updatedTask)
        {
            Tasks oldTask = _context.Tasks.Find(updatedTask.TaskId);
            oldTask.TaskName = updatedTask.TaskName;
            oldTask.TaskDesc = updatedTask.TaskDesc;
            oldTask.TaskDueDate = updatedTask.TaskDueDate;
            oldTask.TaskIsComplete = updatedTask.TaskIsComplete;

            _context.Entry(oldTask).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Update(oldTask);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ToggleTaskComplete(int taskId) 
        {
            Tasks task = _context.Tasks.Find(taskId);
            task.TaskIsComplete = !task.TaskIsComplete;

            _context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Update(task);
            _context.SaveChanges();

            return RedirectToAction("Index");
        } 
    }
}
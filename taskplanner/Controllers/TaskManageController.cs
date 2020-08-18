using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskplanner.Models;
using taskplanner.ViewModels;

namespace taskplanner.Controllers
{

    [Authorize]
    public class TaskManageController : Controller
    {
        // GET: TaskManageController
        private ApplicationContext db;

        public TaskManageController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var UserTasks = db.ScheduledTasks.Where(x => x.IdUser == currentUserID);


            List<ScheduledTaskViewModel> TasksView = new List<ScheduledTaskViewModel>();
            TasksView.AddRange(UserTasks.Where(x => x.ParentId == null)
                                       .Select(i => new ScheduledTaskViewModel(i)));

            List<ScheduledTaskViewModel> scheduledTasks = new List<ScheduledTaskViewModel>(); ;
            scheduledTasks.AddRange(UserTasks.Where(x => x.ParentId != null)
                                       .Select(i => new ScheduledTaskViewModel(i)));


            foreach (var item in TasksView)
            {
                var child = FindChild(item.Id, scheduledTasks);
                if (child.Count != 0)
                {
                    item.ChildTask = new List<ScheduledTaskViewModel>();
                    item.ChildTask.AddRange(child);
                }
            }

            IEnumerable<ScheduledTaskViewModel> data = TasksView;
            return View(data);
        }

        [HttpGet]
        public IActionResult NextChild(ScheduledTaskViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult AddTask(int? id)
        {
            if(id != null)
            {
                ViewBag.ParentId = id;
            }
            
            return View();
        }
        

        [HttpPost]
        public IActionResult AddTask(AddTaskViewModel addTask)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            db.ScheduledTasks.Add(new ScheduledTask()
            {
                IdUser = currentUserID,
                Done = false,
                Name = addTask.Name,
                Text = addTask.Text,
                Deadline = addTask.Deadline,
                ParentId = addTask.ParentId != null ? addTask.ParentId : null
            });

            db.SaveChanges();
            return RedirectToAction("Index", "TaskManage");
        }

        [HttpGet]
        public IActionResult DoneTask(int? id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            db.ScheduledTasks.FirstOrDefault(x => x.Id == id).Done = true;

            db.SaveChanges();
            return RedirectToAction("Index", "TaskManage");
        }


        private List<ScheduledTaskViewModel> FindChild(int Id, List<ScheduledTaskViewModel> scheduledTasks)
        {
            var usingTasks = new List<ScheduledTaskViewModel>(scheduledTasks);

            List<ScheduledTaskViewModel> TasksView = new List<ScheduledTaskViewModel>(usingTasks.Where(x => x.ParentId == Id));

            foreach (var item in TasksView)
            {
                var child = FindChild(item.Id, scheduledTasks);
                if (child != null)
                {
                    item.ChildTask = new List<ScheduledTaskViewModel>();
                    item.ChildTask.AddRange(child);
                }
            }

            return TasksView;
        }


    }
}

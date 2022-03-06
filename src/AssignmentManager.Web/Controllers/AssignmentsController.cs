using AssignmentManager.Services;
using AssignmentManager.Services.Models.Assignment;
using AssignmentManager.Web.Models;
using AssignmentManager.Web.ViewModels.Assignment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentManager.Web.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly IAssignmentsService assignmentsService;

        public AssignmentsController(IAssignmentsService assignmentsService)
        {
            this.assignmentsService = assignmentsService;
        }

        public IActionResult Index()
        {
            var assignments = this.assignmentsService.GetAll();

            return this.View(assignments);
        }

        public IActionResult Completed()
        {
            var assignments = this.assignmentsService.GetAll();

            return this.View(assignments);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateAssignmentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var assignmentServiceModel = new CreateAssignmentServiceModel()
            {
                Name = model.Name,
                ClassId = model.ClassId,
                Description = model.Description,
                DueDate = model.DueDate
            };

            this.assignmentsService.Create(assignmentServiceModel);

            return this.RedirectToAction("Index", "Assignments");
        }


        
        [HttpGet]
        public IActionResult Details(int id)
        {
            var assignment = this.assignmentsService.GetById(id);

            if (assignment.Name == null)
            {
                return BadRequest();
            }

            AssignmentDetailsViewModel viewModel = new AssignmentDetailsViewModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                DueDate = assignment.DueDate,
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted,
            };

            return this.View(viewModel);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            var assignment = this.assignmentsService.GetById(id);

            if (assignment.Name == null)
            {
                return this.BadRequest();
            }

            var viewModel = new AssignmentDetailsViewModel()
            {

                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                DueDate = assignment.DueDate,
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted,
            };

            return this.View(viewModel);

        }

        [HttpPost]
        public IActionResult Edit(AssignmentEditInputModel model)
        {
            if (!this.assignmentsService.Exists(model.Id))
            {
                return this.BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var easm = new EditAssignmentServiceModel()
            {
                Id = model.Id,
                Name = model.Name,
                ClassId = model.ClassId,
                DueDate = model.DueDate,
                Description = model.Description,
                IsCompleted = model.IsCompleted,
            };

            this.assignmentsService.Edit(easm);

            return this.RedirectToAction("Index", "Assignments", new { id = easm.Id });
        }


        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var assignment = this.assignmentsService.GetById(id);

            if (assignment.Name == null)
            {
                return this.BadRequest();
            }

            var cdvm = new AssignmentDetailsViewModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                DueDate = assignment.DueDate,
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted,
            };

            return this.View(cdvm);
        }

        [HttpPost]
        public IActionResult Delete(AssignmentDetailsViewModel model)
        {
            bool success = this.assignmentsService.Remove(model.Id);

            if (!success)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Index", "Assignments");
        }

        [HttpGet]
        public IActionResult Complete(int id)
        {
            var assignment = this.assignmentsService.GetById(id);

            if (assignment.Name == null)
            {
                return this.BadRequest();
            }

            var cdvm = new AssignmentDetailsViewModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                DueDate = assignment.DueDate,
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted,
            };

            return this.View(cdvm);
        }

        [HttpPost]
        public IActionResult Complete(AssignmentDetailsViewModel model)
        {
            this.assignmentsService.Complete(model.Id);

            return this.RedirectToAction("Index", "Assignments");
        }

        [HttpGet]
        public IActionResult Uncomplete(int id)
        {
            var assignment = this.assignmentsService.GetById(id);

            if (assignment.Name == null)
            {
                return this.BadRequest();
            }

            var cdvm = new AssignmentDetailsViewModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                DueDate = assignment.DueDate,
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted,
            };

            return this.View(cdvm);
        }

        [HttpPost]
        public IActionResult Uncomplete(AssignmentDetailsViewModel model)
        {
            this.assignmentsService.Uncomplete(model.Id);

            return this.RedirectToAction("Index", "Assignments");
        }
    }
}

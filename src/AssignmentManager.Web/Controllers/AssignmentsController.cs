using System.Collections.Generic;
using System.Linq;

using AssignmentManager.Data;
using AssignmentManager.Services;
using AssignmentManager.Services.Models.Assignment;
using AssignmentManager.Web.ViewModels.Assignment;

using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Web.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly IClassesService classesService;
        private readonly AssignmentManagerDbContext context;
        

        public AssignmentsController(IAssignmentsService assignmentsService, IClassesService classesService, AssignmentManagerDbContext context)
        {
            this.assignmentsService = assignmentsService;
            this.classesService = classesService;
            this.context = context;
        }

        public IActionResult Index()
        {
            var assignments = this.assignmentsService.GetAll();

            var assignmentsViewModels = new List<AssignmentDetailsViewModel>();

            foreach (var assignment in assignments)
            {
                var assignmentViewModel = new AssignmentDetailsViewModel
                {
                    Id = assignment.Id,
                    Name = assignment.Name,
                    ClassId = assignment.ClassId,
                    ClassName = classesService.GetClassName(assignment.ClassId),
                    ColorHex = classesService.GetColorHex(assignment.ClassId),
                    DueDate = assignment.DueDate,
                    DueDateMessage = assignmentsService.GetDueDateMessage(assignment.DueDate),
                    Description = assignment.Description,
                    IsCompleted = assignment.IsCompleted
                };

                assignmentsViewModels.Add(assignmentViewModel);
            }

            ViewBag.Context = this.context;
            return this.View(assignmentsViewModels);
        }

        public IActionResult Completed()
        {
            var assignments = this.assignmentsService.GetAll();

            var assignmentsViewModels = new List<AssignmentDetailsViewModel>();

            foreach (var assignment in assignments.Where(x => x.IsCompleted))
            {
                var assignmentViewModel = new AssignmentDetailsViewModel
                {
                    Id = assignment.Id,
                    Name = assignment.Name,
                    ClassId = assignment.ClassId,
                    ClassName = classesService.GetClassName(assignment.ClassId),
                    ColorHex = classesService.GetColorHex(assignment.ClassId),
                    DueDate = assignment.DueDate,
                    DueDateMessage = assignmentsService.GetDueDateMessage(assignment.DueDate),
                    Description = assignment.Description,
                    IsCompleted = assignment.IsCompleted
                };

                assignmentsViewModels.Add(assignmentViewModel);
            }

            return this.View(assignmentsViewModels);
        }

        public IActionResult Create()
        {
            ViewBag.Context = this.context;
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
            if (!this.assignmentsService.Exists(id))
            {
                return this.NotFound();
            }

            var assignment = this.assignmentsService.GetById(id);

            if (assignment.Name == null)
            {
                return BadRequest();
            }

            var assignmentViewModel = new AssignmentDetailsViewModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                ClassName = classesService.GetClassName(assignment.ClassId),
                ColorHex = classesService.GetColorHex(assignment.ClassId),
                DueDate = assignment.DueDate,
                DueDateMessage = assignmentsService.GetDueDateMessage(assignment.DueDate),
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted
            };

            ViewBag.Context = this.context;
            return this.View(assignmentViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!this.assignmentsService.Exists(id))
            {
                return this.NotFound();
            }

            var assignment = this.assignmentsService.GetById(id);

            if (assignment.Name == null)
            {
                return this.BadRequest();
            }

            var assignmentViewModel = new AssignmentDetailsViewModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                ClassName = classesService.GetClassName(assignment.ClassId),
                ColorHex = classesService.GetColorHex(assignment.ClassId),
                DueDate = assignment.DueDate,
                DueDateMessage = assignmentsService.GetDueDateMessage(assignment.DueDate),
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted
            };

            ViewBag.Context = this.context;
            return this.View(assignmentViewModel);

        }

        [HttpPost]
        public IActionResult Edit(AssignmentEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var assignmentServiceModel = new EditAssignmentServiceModel()
            {
                Id = model.Id,
                Name = model.Name,
                ClassId = model.ClassId,
                DueDate = model.DueDate,
                Description = model.Description,
                IsCompleted = model.IsCompleted,
            };

            this.assignmentsService.Edit(assignmentServiceModel);

            return this.RedirectToAction("Index", "Assignments", new { id = assignmentServiceModel.Id });
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!this.assignmentsService.Exists(id))
            {
                return this.NotFound();
            }

            var assignment = this.assignmentsService.GetById(id);

            if (assignment.Name == null)
            {
                return this.BadRequest();
            }

            var assignmentViewModel = new AssignmentDetailsViewModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                ClassName = classesService.GetClassName(assignment.ClassId),
                ColorHex = classesService.GetColorHex(assignment.ClassId),
                DueDate = assignment.DueDate,
                DueDateMessage = assignmentsService.GetDueDateMessage(assignment.DueDate),
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted
            };

            return this.View(assignmentViewModel);
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
            if (!this.assignmentsService.Exists(id))
            {
                return this.NotFound();
            }

            var assignment = this.assignmentsService.GetById(id);

            if (assignment.Name == null)
            {
                return this.BadRequest();
            }

            var assignmentViewModel = new AssignmentDetailsViewModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                ClassName = classesService.GetClassName(assignment.ClassId),
                ColorHex = classesService.GetColorHex(assignment.ClassId),
                DueDate = assignment.DueDate,
                DueDateMessage = assignmentsService.GetDueDateMessage(assignment.DueDate),
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted
            };

            return this.View(assignmentViewModel);
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
            if (!this.assignmentsService.Exists(id))
            {
                return this.NotFound();
            }

            var assignment = this.assignmentsService.GetById(id);

            if (assignment.Name == null)
            {
                return this.BadRequest();
            }

            var assignmentViewModel = new AssignmentDetailsViewModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                ClassName = classesService.GetClassName(assignment.ClassId),
                ColorHex = classesService.GetColorHex(assignment.ClassId),
                DueDate = assignment.DueDate,
                DueDateMessage = assignmentsService.GetDueDateMessage(assignment.DueDate),
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted
            };

            return this.View(assignmentViewModel);
        }

        [HttpPost]
        public IActionResult Uncomplete(AssignmentDetailsViewModel model)
        {
            this.assignmentsService.Uncomplete(model.Id);

            return this.RedirectToAction("Index", "Assignments");
        }
    }
}

using System.Collections.Generic;

using AssignmentManager.Data;
using AssignmentManager.Services;
using AssignmentManager.Services.Implementations;
using AssignmentManager.Services.Models.Class;
using AssignmentManager.Web.ViewModels.Assignment;
using AssignmentManager.Web.ViewModels.Class;

using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Web.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly IClassesService classesService;
        private readonly AssignmentManagerDbContext context;


        public ClassesController(IAssignmentsService assignmentsService, IClassesService classesService, AssignmentManagerDbContext context)
        {
            this.assignmentsService = assignmentsService;
            this.classesService = classesService;
            this.context = context;
        }

        public IActionResult Index()
        {
            var classes = this.classesService.GetAll();

            return this.View(classes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateClassInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var classServiceModel = new CreateClassServiceModel()
            {
                Name = model.Name,
                ColorId = model.ColorId
            };

            this.classesService.Create(classServiceModel);

            return this.RedirectToAction("Index", "Classes");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!this.classesService.Exists(id))
            {
                return this.NotFound();
            }

            var classObj = this.classesService.GetById(id);

            if (classObj.Name == null)
            {
                return BadRequest();
            }

            var classViewModel = new ClassDetailsViewModel()
            {
                Id = classObj.Id,
                Name = classObj.Name,
                Color = classObj.Color
            };

            var assignments = this.classesService.GetAllAssignmentsByClassId(id);

            var assignmentsViewModels = new List<AssignmentDetailsViewModel>();
            foreach (var assignment in assignments)
            {
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

                assignmentsViewModels.Add(assignmentViewModel);
            }

            classViewModel.Assignments = assignmentsViewModels;

            return this.View(classViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!this.classesService.Exists(id))
            {
                return this.NotFound();
            }

            var classObj = this.classesService.GetById(id);

            if (classObj.Name == null)
            {
                return this.BadRequest();
            }

            var classViewModel = new ClassDetailsViewModel()
            {
                Id = classObj.Id,
                Name = classObj.Name,
                Color = classObj.Color,
            };

            ViewBag.Context = this.context;
            return this.View(classViewModel);
            
        }

        [HttpPost]
        public IActionResult Edit(ClassEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var classServiceModel = new EditClassServiceModel()
            {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
            };

            this.classesService.Edit(classServiceModel);

            return this.RedirectToAction("Index", "Classes", new { id = classServiceModel.Id });
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!this.classesService.Exists(id))
            {
                return this.NotFound();
            }

            var classObj = this.classesService.GetById(id);

            if (classObj.Name == null)
            {
                return this.BadRequest();
            }

            var classViewModel = new ClassDetailsViewModel()
            {
                Id = classObj.Id,
                Name = classObj.Name,
                Color = classObj.Color,
            };

            return this.View(classViewModel);
        }

        [HttpPost]
        public IActionResult Delete(ClassDetailsViewModel model)
        {
            var success = this.classesService.Remove(model.Id);

            if (!success)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Index", "Classes");
        }
        public IActionResult Welcome()
        {
            return View();
        }
    }
}

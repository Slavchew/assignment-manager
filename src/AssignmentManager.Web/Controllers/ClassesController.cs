using System.Collections.Generic;

using AssignmentManager.Services;
using AssignmentManager.Services.Models.Class;
using AssignmentManager.Web.ViewModels.Assignment;
using AssignmentManager.Web.ViewModels.Class;

using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Web.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IClassesService classesService;

        public ClassesController(IClassesService classesService)
        {
            this.classesService = classesService;
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
                return this.BadRequest();
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

            var assignmentsViewModel = new List<AssignmentDetailsViewModel>();
            foreach (var item in assignments)
            {
                var assignment = new AssignmentDetailsViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ClassId = item.ClassId,
                    DueDate = item.DueDate,
                    Description = item.Description,
                };

                assignmentsViewModel.Add(assignment);
            }

            classViewModel.Assignments = assignmentsViewModel;

            return this.View(classViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!this.classesService.Exists(id))
            {
                return this.BadRequest();
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
        public IActionResult Edit(ClassEditInputModel model)
        {
            if (!this.classesService.Exists(model.Id))
            {
                return this.BadRequest();
            }

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
                return this.BadRequest();
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

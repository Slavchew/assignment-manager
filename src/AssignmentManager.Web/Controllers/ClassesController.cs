using AssignmentManager.Services;
using AssignmentManager.Services.Models.Class;
using AssignmentManager.Web.ViewModels.Assignment;
using AssignmentManager.Web.ViewModels.Class;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            var classObj = this.classesService.GetById(id);

            if (classObj.Name == null)
            {
                return BadRequest();
            }

            ClassDetailsViewModel viewModel = new ClassDetailsViewModel()
            {
                Id = classObj.Id,
                Name = classObj.Name,
                Color = classObj.Color
            };

            var assignments = this.classesService.GetAllAssignmentsByClassId(id);

            List<AssignmentDetailsViewModel> assignmentsViewModel = new List<AssignmentDetailsViewModel>();
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

            viewModel.Assignments = assignmentsViewModel;

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            var classObj = this.classesService.GetById(id);

            if (classObj.Name == null)
            {
                return this.BadRequest();
            }

            var viewModel = new ClassDetailsViewModel()
            {
                Id = classObj.Id,
                Name = classObj.Name,
                Color = classObj.Color,
            };

            return this.View(viewModel);
            
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

            var ecsm = new EditClassServiceModel()
            {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
            };

            this.classesService.Edit(ecsm);

            return this.RedirectToAction("Index", "Classes", new { id = ecsm.Id });
        }

        
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = this.classesService.GetById(id);

            if (category.Name == null)
            {
                return this.BadRequest();
            }

            var cdvm = new ClassDetailsViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
            };

            return this.View(cdvm);
        }

        [HttpPost]
        public IActionResult Delete(ClassDetailsViewModel model)
        {
            bool success = this.classesService.Remove(model.Id);

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

using System;
using System.Collections.Generic;
using System.Linq;

using AssignmentManager.Data;
using AssignmentManager.Data.Models;
using AssignmentManager.Services.Models.Assignment;
using AssignmentManager.Services.Models.Class;

using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Services.Implementations
{
    public class ClassesService : IClassesService
    {
        private AssignmentManagerDbContext db;

        public ClassesService(AssignmentManagerDbContext db)
        {
            this.db = db;
        }

        public DetailsClassServiceModel GetById(int id)
        {
            var classObj = this.db.Classes
                .Include(x => x.Color)
                .Include(x => x.Assignments)
                .FirstOrDefault(x => x.Id == id);

            if (classObj == null)
            {
                throw new InvalidOperationException();
            }

            var classModel = new DetailsClassServiceModel()
            {
                Id = classObj.Id,
                Name = classObj.Name,
                Color = classObj.Color.Name,
            };

            List<DetailsAssignmentServiceModel> assignments = new List<DetailsAssignmentServiceModel>();

            foreach (var item in classObj.Assignments)
            {
                var assignment = new DetailsAssignmentServiceModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ClassId = item.ClassId,
                    DueDate = item.DueDate,
                    Description = item.Description,
                    IsCompleted = item.IsCompleted
                };

                assignments.Add(assignment);
            }

            classModel.Assignments = assignments;

            return classModel;
        }

        public void Create(CreateClassServiceModel model)
        {
            var color = this.db.Colors.FirstOrDefault(x => x.Id == model.ColorId);

            if (color == null)
            {
                throw new InvalidOperationException("Color cannot be found");
            }

            var classObj = new Class()
            {
                Name = model.Name,
                Color = color,
            };

            this.db.Classes.Add(classObj);
            this.db.SaveChanges();
        }

        public void Edit(EditClassServiceModel model)
        {
            var classObj = this.db
                .Classes
                .Include(x => x.Color)
                .FirstOrDefault(c => c.Id == model.Id);

            var color = this.db.Colors.FirstOrDefault(x => x.Name == model.Color);

            if (color == null)
            {
                throw new InvalidOperationException("Color cannot be found");
            }

            classObj.Name = model.Name;
            classObj.Color = color;

            this.db.SaveChanges();
        }

        public bool Remove(int id)
        {
            var classObj = this.db
                .Classes
                .FirstOrDefault(c => c.Id == id);

            if (classObj == null)
            {
                return false;
            }

            this.db.Classes.Remove(classObj);
            int deletedEntitiesCount = this.db.SaveChanges();

            if (deletedEntitiesCount == 0)
            {
                return false;
            }

            return true;
        }

        public bool Exists(int id)
        {
            return this.db
                .Classes
                .Any(c => c.Id == id);
        }

        public IEnumerable<DetailsClassServiceModel> GetAll()
        {
            return this.db.Classes
                .Include(x => x.Assignments)
                .Select(x => new DetailsClassServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Color = x.Color.Name,
                    Assignments = x.Assignments
                        .Select(a => new DetailsAssignmentServiceModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ClassId = a.ClassId,
                            DueDate = a.DueDate,
                            Description = a.Description,
                            IsCompleted = a.IsCompleted
                        })
                        .ToList()
                })
                .OrderBy(x => x.Id)
                .ToList();
        }

        public IEnumerable<DetailsAssignmentServiceModel> GetAllAssignmentsByClassId(int id)
        {
            return this.db.Assignments
                .Where(x => x.ClassId == id)
                .Select(x => new DetailsAssignmentServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ClassId = x.ClassId,
                    DueDate = x.DueDate,
                    Description = x.Description,
                })
                .OrderByDescending(x => x.DueDate)
                .ToList();
        }

        public string GetColorHex(int classId)
        {
            var assignmentClass = db.Classes.FirstOrDefault(x => x.Id == classId);
            var classColorId = assignmentClass.ColorId;
            var classColor = db.Colors.FirstOrDefault(x => x.Id == classColorId);

            return classColor.Name;
        }

        public string GetClassName(int classId)
        {
            return db.Classes.FirstOrDefault(x => x.Id == classId).Name;
        }
    }
}

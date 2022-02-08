using AssignmentManager.Data;
using AssignmentManager.Data.Models;
using AssignmentManager.Services.Models.Assignment;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Services.Implementations
{
    public class AssignmentsService : IAssignmentsService
    {
        private AssignmentManagerDbContext db;

        public AssignmentsService(AssignmentManagerDbContext db)
        {
            this.db = db;
        }

        public DetailsAssignmentServiceModel GetById(int id)
        {
            var assignment = this.db.Assignments
                .Include(x => x.Class)
                .FirstOrDefault(x => x.Id == id);

            var dcsm = new DetailsAssignmentServiceModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                DueDate = assignment.DueDate,
                Description = assignment.Description,
            };

            return dcsm;
        }

        public void Create(CreateAssignmentServiceModel model)
        {
            var classObj = this.db.Classes.FirstOrDefault(x => x.Id == model.ClassId);

            var assignment = new Assignment()
            {
                Name = model.Name,
                Class = classObj,
                DueDate = model.DueDate,
                Description = model.Description,
            };

            this.db.Assignments.Add(assignment);
            this.db.SaveChanges();
        }

        public void Edit(EditAssignmentServiceModel model)
        {
            var assignment = this.db
                .Assignments
                .Include(x => x.Class)
                .FirstOrDefault(a => a.Id == model.Id);

            var classObj = this.db.Classes.FirstOrDefault(x => x.Id == model.ClassId);

            if (classObj == null)
            {
                throw new InvalidOperationException("Class cannot be found");
            }

            assignment.Name = model.Name;
            assignment.Class = classObj;
            assignment.DueDate = model.DueDate;
            assignment.Description = model.Description;

            this.db.SaveChanges();
        }

        public bool Remove(int id)
        {
            var assignment = this.db
                .Assignments
                .Include(x => x.Class)
                .FirstOrDefault(a => a.Id == id);

            if (assignment == null)
            {
                return false;
            }

            this.db.Assignments.Remove(assignment);
            int deletedEntitiesCount = this.db.SaveChanges();

            if (deletedEntitiesCount == 0)
            {
                return false;
            }

            return true;
        }

        public bool Exists(int assignmentId)
        {
            return this.db
                .Assignments
                .Any(a => a.Id == assignmentId);
        }

        public IEnumerable<DetailsAssignmentServiceModel> GetAll()
        {
            return this.db.Assignments
                .Select(x => new DetailsAssignmentServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ClassId = x.ClassId,
                    Description = x.Description,
                    DueDate = x.DueDate,
                    IsCompleted = x.IsCompleted
                })
                .OrderBy(x => x.Id)
                .ToList();
        }
    }
}

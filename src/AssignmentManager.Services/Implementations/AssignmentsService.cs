using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using AssignmentManager.Data;
using AssignmentManager.Data.Models;
using AssignmentManager.Services.Models.Assignment;

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

            if (assignment == null)
            {
                throw new InvalidOperationException();
            }

            var assignmentModel = new DetailsAssignmentServiceModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                ClassId = assignment.ClassId,
                DueDate = assignment.DueDate,
                Description = assignment.Description,
                IsCompleted = assignment.IsCompleted,
            };

            return assignmentModel;
        }

        public void Create(CreateAssignmentServiceModel model)
        {
            var classObj = this.db.Classes.FirstOrDefault(x => x.Id == model.ClassId);

            if (classObj == null)
            {
                throw new InvalidOperationException("Class cannot be found");
            }

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

        public void Complete(int id)
        {
            var assignment = this.db
                .Assignments
                .Include(x => x.Class)
                .FirstOrDefault(a => a.Id == id);

            if (assignment == null)
            {
                throw new InvalidOperationException();
            }

            assignment.IsCompleted = true;
            this.db.SaveChanges();
        }

        public void Uncomplete(int id)
        {
            var assignment = this.db
                .Assignments
                .Include(x => x.Class)
                .FirstOrDefault(a => a.Id == id);

            if (assignment == null)
            {
                throw new InvalidOperationException();
            }

            assignment.IsCompleted = false;
            this.db.SaveChanges();
        }

        public bool Exists(int id)
        {
            return this.db
                .Assignments
                .Any(a => a.Id == id);
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
                .OrderBy(x => x.DueDate)
                .ToList();
        }

        public string GetDueDateMessage(DateTime dueDate)
        {
            string dueDateMessage = "";
            var now = DateTime.UtcNow;

            if (dueDate.Date.AddDays(1) == now.Date)
                dueDateMessage = "Due yesterday";
            else if (dueDate.Date.AddDays(2) == now.Date)
                dueDateMessage = "Due 2 days ago";
            else if (dueDate.Date.AddDays(3) == now.Date)
                dueDateMessage = "Due 3 days ago";
            else if (dueDate.Date.AddDays(4) == now.Date)
                dueDateMessage = "Due 4 days ago";
            else if (dueDate.Date.AddDays(5) == now.Date)
                dueDateMessage = "Due 5 days ago";
            else if (dueDate.Date.AddDays(6) == now.Date)
                dueDateMessage = "Due 6 days ago";
            else if (dueDate.Date.AddDays(7) == now.Date)
                dueDateMessage = "Due a week ago";
            else if (now.Date == dueDate.Date)
                dueDateMessage = "Due today";
            else if (dueDate.Date == now.Date.AddDays(1))
                dueDateMessage = "Due tomorrow";
            else if (dueDate.Date == now.Date.AddDays(2))
                dueDateMessage = "Due in 2 days";
            else if (dueDate.Date == now.Date.AddDays(3))
                dueDateMessage = "Due in 3 days";
            else if (dueDate.Date == now.Date.AddDays(4))
                dueDateMessage = "Due in 4 days";
            else if (dueDate.Date == now.Date.AddDays(5))
                dueDateMessage = "Due in 5 days";
            else if (dueDate.Date == now.Date.AddDays(6))
                dueDateMessage = "Due in 6 days";
            else if (dueDate.Date == now.Date.AddDays(7))
                dueDateMessage = "Due in a week";
            else
                dueDateMessage = $"Due {dueDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}";

            return dueDateMessage;
        }
    }
}

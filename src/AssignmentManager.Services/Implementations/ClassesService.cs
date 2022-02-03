using AssignmentManager.Data;
using AssignmentManager.Data.Models;
using AssignmentManager.Services.Models.Class;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
                .FirstOrDefault(x => x.Id == id);

            var dcsm = new DetailsClassServiceModel()
            {
                Id = classObj?.Id,
                Name = classObj?.Name,
                Color = classObj?.Color.Name,
            };

            return dcsm;
        }

        public void Create(CreateClassServiceModel model)
        {
            var color = this.db.Colors.FirstOrDefault(x => x.Id == model.ColorId);

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
                .Include(x => x.Color)
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

        public bool Exists(int classId)
        {
            return this.db
                .Classes
                .Any(c => c.Id == classId);
        }

        public IEnumerable<DetailsClassServiceModel> GetAll()
        {
            return this.db.Classes
                .Select(x => new DetailsClassServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Color = x.Color.Name
                })
                .OrderBy(x => x.Id)
                .ToList();
        }
    }
}

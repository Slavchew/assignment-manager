using AssignmentManager.Services.Models.Class;
using System.Collections.Generic;

namespace AssignmentManager.Services
{
    public interface IClassesService
    {
        DetailsClassServiceModel GetById(int id);

        void Create(CreateClassServiceModel model);

        void Edit(EditClassServiceModel model);

        bool Remove(int id);

        bool Exists(int classId);

        IEnumerable<DetailsClassServiceModel> GetAll();
    }
}

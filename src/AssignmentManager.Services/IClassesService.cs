using System.Collections.Generic;

using AssignmentManager.Services.Models.Assignment;
using AssignmentManager.Services.Models.Class;

namespace AssignmentManager.Services
{
    public interface IClassesService
    {
        DetailsClassServiceModel GetById(int id);

        void Create(CreateClassServiceModel model);

        void Edit(EditClassServiceModel model);

        bool Remove(int id);

        bool Exists(int id);

        IEnumerable<DetailsClassServiceModel> GetAll();

        IEnumerable<DetailsAssignmentServiceModel> GetAllAssignmentsByClassId(int id);

        string GetColorHex(int classId);

        string GetClassName(int classId);
    }
}

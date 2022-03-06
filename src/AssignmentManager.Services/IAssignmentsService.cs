using AssignmentManager.Services.Models.Assignment;
using System.Collections.Generic;

namespace AssignmentManager.Services
{
    public interface IAssignmentsService
    {
        DetailsAssignmentServiceModel GetById(int id);

        void Create(CreateAssignmentServiceModel model);

        void Edit(EditAssignmentServiceModel model);

        bool Remove(int id);

        void Complete(int id);

        void Uncomplete(int id);

        bool Exists(int assignmentId);

        IEnumerable<DetailsAssignmentServiceModel> GetAll();
    }
}

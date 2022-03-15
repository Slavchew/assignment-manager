using System;
using System.Collections.Generic;

using AssignmentManager.Services.Models.Assignment;

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

        bool Exists(int id);

        IEnumerable<DetailsAssignmentServiceModel> GetAll();

        string GetDueDateMessage(DateTime dueDate);
    }
}

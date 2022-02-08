using AssignmentManager.Services.Models.Assignment;
using System.Collections.Generic;

namespace AssignmentManager.Services.Models.Class
{
    public class DetailsClassServiceModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public ICollection<DetailsAssignmentServiceModel> Assignments { get; set; }
    }
}

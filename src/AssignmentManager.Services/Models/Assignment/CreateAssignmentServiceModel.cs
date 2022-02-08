using System;

namespace AssignmentManager.Services.Models.Assignment
{
    public class CreateAssignmentServiceModel
    {
        public string Name { get; set; }

        public int ClassId { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }
    }
}

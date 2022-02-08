using System;

namespace AssignmentManager.Web.ViewModels.Assignment
{
    public class CreateAssignmentInputModel
    {
        public string Name { get; set; }

        public int ClassId { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }
    }
}

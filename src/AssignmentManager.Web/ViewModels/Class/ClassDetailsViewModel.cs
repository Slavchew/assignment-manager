using System.Collections.Generic;
using AssignmentManager.Web.ViewModels.Assignment;

namespace AssignmentManager.Web.ViewModels.Class
{
    public class ClassDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public ICollection<AssignmentDetailsViewModel> Assignments { get; set; }
    }
}

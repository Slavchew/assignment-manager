using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentManager.Web.ViewModels.Assignment;

namespace AssignmentManager.Web.ViewModels.Class
{
    public class ClassDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Color { get; set; }

        public ICollection<AssignmentDetailsViewModel> Assignments { get; set; }
    }
}

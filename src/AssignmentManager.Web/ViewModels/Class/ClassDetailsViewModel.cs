using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentManager.Web.ViewModels.Assignment;

namespace AssignmentManager.Web.ViewModels.Class
{
    public class ClassDetailsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "My custom error message.")]
        [MaxLength(50, ErrorMessage = "My custom error message.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "My custom error message.")]
        public string Color { get; set; }

        public ICollection<AssignmentDetailsViewModel> Assignments { get; set; }
    }
}

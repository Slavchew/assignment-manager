using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using AssignmentManager.Web.ViewModels.Assignment;

namespace AssignmentManager.Web.ViewModels.Class
{
    public class ClassDetailsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(50, ErrorMessage = "Character limit is 50.")]
        [RegularExpression(@".*\S.*", ErrorMessage = "No whitespace allowed.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Choosing a color is required.")]
        public string Color { get; set; }

        public ICollection<AssignmentDetailsViewModel> Assignments { get; set; }
    }
}

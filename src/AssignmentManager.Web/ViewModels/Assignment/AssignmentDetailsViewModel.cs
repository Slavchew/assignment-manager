using System;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Web.ViewModels.Assignment
{
    public class AssignmentDetailsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(50, ErrorMessage = "Character limit is 50.")]
        [RegularExpression(@".*\S.*", ErrorMessage = "No whitespace allowed.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Class is required.")]
        public int ClassId { get; set; }

        public string ClassName { get; set; }

        public string ColorHex { get; set; }

        [Required(ErrorMessage = "Due Date is required.")]
        public DateTime DueDate { get; set; }

        public string DueDateMessage { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}

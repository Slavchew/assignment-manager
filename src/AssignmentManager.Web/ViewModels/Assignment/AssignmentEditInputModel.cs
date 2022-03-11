using System;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Web.ViewModels.Assignment
{
    public class AssignmentEditInputModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int ClassId { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}

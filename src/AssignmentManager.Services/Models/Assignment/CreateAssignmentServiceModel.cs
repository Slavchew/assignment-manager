using System;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Services.Models.Assignment
{
    public class CreateAssignmentServiceModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int ClassId { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }
    }
}

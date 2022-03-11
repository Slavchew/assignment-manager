using System;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Data.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "My custom error message.")]
        [MaxLength(50, ErrorMessage = "My custom error message.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "My custom error message.")]
        public int ClassId { get; set; }

        public virtual Class Class { get; set; }

        [Required(ErrorMessage = "My custom error message.")]
        public DateTime DueDate { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Data.Models
{
    public class Class
    {
        public Class()
        {
            this.Assignments = new HashSet<Assignment>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(50, ErrorMessage = "Character limit is 50.")]
        [RegularExpression(@".*\S.*", ErrorMessage = "No whitespace allowed.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Choosing a color is required.")]
        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Data.Models
{
    public class Class
    {
        public Class()
        {
            this.Assignments = new HashSet<Assignment>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}

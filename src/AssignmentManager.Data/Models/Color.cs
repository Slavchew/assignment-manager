using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Data.Models
{
    public class Color
    {
        public Color()
        {
            this.Classes = new HashSet<Class>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Data.Models
{
    public class Class
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int ColorId { get; set; }

        public Color Color { get; set; }
    }
}

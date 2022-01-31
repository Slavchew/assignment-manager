using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Data.Models
{
    public class Color
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
    }
}

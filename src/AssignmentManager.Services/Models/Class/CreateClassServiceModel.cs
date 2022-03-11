using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Services.Models.Class
{
    public class CreateClassServiceModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int ColorId { get; set; }
    }
}

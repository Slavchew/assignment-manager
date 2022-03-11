using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Services.Models.Class
{
    public class EditClassServiceModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Color { get; set; }
    }
}

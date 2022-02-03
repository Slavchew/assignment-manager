using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Web.ViewModels.Class
{
    public class CreateClassInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int ColorId { get; set; }
    }
}

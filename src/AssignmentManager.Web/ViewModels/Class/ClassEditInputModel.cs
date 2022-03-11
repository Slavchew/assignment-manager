using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Web.ViewModels.Class
{
    public class ClassEditInputModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Color { get; set; }
    }
}

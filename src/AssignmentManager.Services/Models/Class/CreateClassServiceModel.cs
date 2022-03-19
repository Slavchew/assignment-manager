using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

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

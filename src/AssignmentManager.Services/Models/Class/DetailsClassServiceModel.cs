using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using AssignmentManager.Services.Models.Assignment;

namespace AssignmentManager.Services.Models.Class
{
    public class DetailsClassServiceModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Color { get; set; }

        public ICollection<DetailsAssignmentServiceModel> Assignments { get; set; }
    }
}

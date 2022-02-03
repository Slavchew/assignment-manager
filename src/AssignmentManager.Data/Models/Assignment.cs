﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Data.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int ClassId { get; set; }

        public virtual Class Class { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}

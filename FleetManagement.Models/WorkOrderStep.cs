using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public class WorkOrderStep
    {
        public int Id { get; set; }

        [Required]
        public DateTime TakenAt { get; set; }

        [MaxLength(200)]
        [Required]
        public string Description { get; set; }

        public ICollection<PaperDocument> Files { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public WorkOrderStep()
        {
            TakenAt = DateTime.Now;
        }
    }
}

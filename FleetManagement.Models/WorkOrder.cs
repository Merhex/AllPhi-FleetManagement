
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Models
{
    public abstract class WorkOrder<T>
        where T : class
    {
        public int Id { get; set; }

        [Required]
        public T Subject { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        [Required]
        public WorkOrderStatus Status { get; set; }

        [Required]
        public Person Handler { get; set; }

        [Required]
        public Person Requester { get; set; }

        public ICollection<WorkOrderStep> Steps { get; set; }

        public WorkOrder(T subject)
        {
            Subject = subject;
            CreationDate = DateTime.Now;
        }
    }
}

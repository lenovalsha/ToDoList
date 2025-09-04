using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace ToDoList
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        public DateTime? DueDate { get; set; }
        public string Task { get; set; }

        //Foreign key for status
        public Status Status { get; set; }
        public int StatusId { get; set; }

        //foreign key for importance
        public Importance Importance { get; set; }
        public int ImportanceId { get; set; }

        public DateTime CompletedDate { get; set; }


        //ICollection is used as a many-to-many relationship
        //public ICollection<Importance> Importance { get; set; }
        //public ICollection< Status> Status { get; set; }

    }
}
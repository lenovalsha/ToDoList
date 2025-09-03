using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace ToDoList
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        public DatePicker Date { get; set; }
        public string Task { get; set; }
        public Importance Importance { get; set; }
        public Status Status { get; set; }

    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace ToDoList
{
    public class Importance
    {
        [Key]
        public  int Id { get; set; }
        public string  Important { get; set; }

        //One Importance can be linked to many tasks
        public ICollection<Tasks> Tasks { get; set; }
    }
}
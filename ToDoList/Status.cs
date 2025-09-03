using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //One status can be linked to many tasks
        public ICollection<Tasks> Tasks { get; set; }
    }
}
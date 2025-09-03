using System.ComponentModel.DataAnnotations;

namespace ToDoList
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
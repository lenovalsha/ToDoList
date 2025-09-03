using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace ToDoList
{
    public class Importance
    {
        [Key]
        public  int Id { get; set; }
        public string  Important { get; set; }
    }
}
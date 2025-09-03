using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//automatically calles when it needs to know how to connect to your database
        {
                           //use SQLite as the database ("the name of the database file")
            optionsBuilder.UseSqlite("Data source = Leila'sTodoList.db ");
            
        }

        //since this context class us used to MAP to the database
        //we need to define our table in the database

        //EACH REPRESENTS A TABLE IN OUR DATABASE
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Importance> Importances { get; set; }
    }
}

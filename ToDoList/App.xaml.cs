using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
//ADD This
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //This method executes everytime the app startsup
        protected override void OnStartup(StartupEventArgs e)
        {
            //CREATE A DATABASE FILE IF IT DOESNT EXIST
            DatabaseFacade facade = new DatabaseFacade(new DataContext());
            facade.EnsureCreated();//This method will ensure that the database file is created if it doesnt exist
        }
    }
}

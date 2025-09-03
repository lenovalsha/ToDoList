using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Status> statusList;
        private List<Importance> importanceList;
        private List<Tasks> tasksList;

        public MainWindow()
        {
            InitializeComponent();
            CreateStatus();
            CreateImportance();
            ReadTasks();
        }
        public void CreateStatus()
        {
            using (DataContext context = new DataContext())
            {
                //Make the statuses if it doesn't exist

                string[] statuses = { "Not Started", "In-progress", "Completed" };

                foreach (string stat in statuses)
                {
                    //Check if it exists
                    bool exist = context.Statuses.Any(s => s.Name == stat);
                    if (!exist)
                    {
                        context.Statuses.Add(new Status() { Name = stat });
                    }
                }
                context.SaveChanges();
            }
            ReadStatus();
        }
        public void ReadStatus()
        {
            using (DataContext context = new DataContext())
            {
                statusList = context.Statuses.ToList();
                lstStatus.ItemsSource = statusList;
            }
        }
        public void CreateImportance()
        {
            using (DataContext context = new DataContext())
            {
                //Make the statuses if it doesn't exist

                string[] imporatances = { "Low", "Medium", "High" };

                foreach (string imp in imporatances)
                {
                    //Check if it exists
                    bool exist = context.Importances.Any(s => s.Name == imp);
                    if (!exist)
                    {
                        context.Importances.Add(new Importance() { Name = imp });
                    }
                }
                context.SaveChanges();
            }
            ReadImportance();
        }
        public void ReadImportance()
        {
            using (DataContext context = new DataContext())
            {
                importanceList = context.Importances.ToList();
                lstImportance.ItemsSource = importanceList; //Will Delete later
                cmbImportance.ItemsSource = importanceList;
                cmbImportance.DisplayMemberPath = "Name";
            }
        }
        public void CreateTask()
        {
            using (DataContext context = new DataContext())
            {
                var date = DateTime.Now;
                var task = txtTask.Text;

                //will need to add error handling
                context.Tasks.Add(new Tasks() { Date = date, Task = task, StatusId = 1, ImportanceId = cmbImportance.SelectedIndex + 1 });
                context.SaveChanges();
            }
            ReadTasks();
        }
        public void ReadTasks()
        {
            using (DataContext context = new DataContext())
            {
                tasksList = context.Tasks
                    .Include(t => t.Status)
                    .Include(t => t.Importance).
                    ToList();
                lstTasks.ItemsSource = tasksList;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateTask();
        }
    }
}

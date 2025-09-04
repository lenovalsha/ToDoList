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
                cmbImportance.ItemsSource = importanceList;
                cmbImportance.DisplayMemberPath = "Name";
            }
        }
        public void SelectedTask()
        {
            using (DataContext context = new DataContext())
            {
                if (lstTasks.SelectedItem != null)
                {
                    Tasks selectedTask = lstTasks.SelectedItem as Tasks;//Will select the task ansd convert it to Task type so we can access the ID
                    txtTask.Text = selectedTask.Task;
                    cmbImportance.SelectedIndex = selectedTask.ImportanceId - 1;//will need to find a way to fix this later
                    dpDate.SelectedDate = selectedTask.DueDate;
                    if (selectedTask.StatusId == 1)//not started
                    {
                        btnStartComplete.Visibility = Visibility.Visible;
                        btnStartComplete.Content = "Start";

                    }
                    else if (selectedTask.StatusId == 2)
                    {
                        btnStartComplete.Content = "Complete";

                    }
                    btnUpdate.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                }
            }
        }
        #region CRUD
        public void CreateTask()
        {
            using (DataContext context = new DataContext())
            {
                var date = dpDate.SelectedDate;
                var task = txtTask.Text;
                if (task != string.Empty || date != null || cmbImportance.SelectedItem != null)
                {
                    //will need to add error handling
                    context.Tasks.Add(new Tasks() { DueDate = date, Task = task, StatusId = 1, ImportanceId = cmbImportance.SelectedIndex + 1 });
                    context.SaveChanges();
                }
            }
            ReadTasks();
            btnUpdate.IsEnabled = false;

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
        public void UpdateTask()
        {
            using (DataContext context = new DataContext())
            {
                Tasks selectedTask = lstTasks.SelectedItem as Tasks;
                var updTask = txtTask.Text;
                var updImportance = cmbImportance.SelectedIndex + 1;
                if (selectedTask != null)
                {
                    Tasks task = context.Tasks.Find(selectedTask.Id);
                    task.Task = updTask;
                    task.ImportanceId = updImportance;
                    context.SaveChanges();
                }
            }
            ReadTasks();
            btnUpdate.IsEnabled = false;
        }
        public void DeleteTask()
        {
            using (DataContext context = new DataContext())
            {
                Tasks selectedTask = lstTasks.SelectedItem as Tasks;
                if (selectedTask != null)
                {
                    Tasks task = context.Tasks.Find(selectedTask.Id);

                    context.Tasks.Remove(task);
                    context.SaveChanges();
                }
            }
            ReadTasks();
        }
        #endregion
        #region StartCompleteTasks
        public void StartComplete()
        {
            using (DataContext context = new DataContext())
            {
                Tasks selectedTask = lstTasks.SelectedItem as Tasks;
                Tasks task = context.Tasks.Find(selectedTask.Id);

                if (lstTasks.SelectedItem != null)
                {

                    if (task.StatusId == 1)//has not started yet
                    {
                        task.StatusId = 2;//start it
                    }
                    else if (task.StatusId == 2)//if its already in progress
                    {
                        task.StatusId = 3;//complete it
                        task.CompletedDate = DateTime.Now;
                    }
                }
                context.SaveChanges();
            }
            ReadTasks();
        }

        #endregion

        #region Buttons
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateTask();
        }

        private void lstImportance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void lstTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedTask();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateTask();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteTask();
        }

        private void btnStartComplete_Click(object sender, RoutedEventArgs e)
        {
            StartComplete();
        }
        #endregion
    }
}

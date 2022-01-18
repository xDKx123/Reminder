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
using Xceed.Wpf.Toolkit;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for TaskControl.xaml
    /// </summary>
    public partial class TaskControl : UserControl
    {
        public int task_list_id;
        public int id;
        public Task task;
        public Action getItems;
        public TaskControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public TaskControl(Task task, int task_list_id, Action getItems)
        {
            InitializeComponent();
            Task = task;
            Id = task.Id;
            this.DataContext = this;
            JobName = task.Name;
            DueDate = task.Deadline;
            IsComplete = task.Completed;
            Description = task.Description;
            this.task_list_id = task_list_id;
            this.getItems = getItems;
        }

        public TaskControl(string jName, DateTime dueDate, bool isdone)
        {
            InitializeComponent();
            this.DataContext = this;
            JobName = jName;
            DueDate = dueDate;
            IsComplete = isdone;
        }

        public TaskControl(int id, string jName, DateTime dueDate, bool isdone, string description, int task_list_id)
        {
            InitializeComponent();
            this.DataContext = this;
            Id = id;
            JobName = jName;
            DueDate = dueDate;
            IsComplete = isdone;
            Description = description;
            this.task_list_id = task_list_id;
        }

        public string JobName {
            get { return TextBlockName.Text; }
            set { TextBlockName.Text = value; }
        }

        public string Description
        {
            get { return TextBlockDescription.Text; }
            set { TextBlockDescription.Text = value; }
        }

        public DateTime DueDate {
            get {
                string tmp = TextBlockDueDate.Text;
                return DateTime.Parse(tmp);
            }
            set { TextBlockDueDate.Text = value.ToString("dd.MM.yyyy"); }
        }

        public Task Task
        {
            get
            {
                return task;
            }
            set { task = value; }
        }

        public bool IsComplete {
            get { return (bool)CheckboxIsCompleted.IsChecked; }
            set { CheckboxIsCompleted.IsChecked = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private async void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await TaskRepository.deleteTask(id);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private async void CheckboxIsCompleted_Checked(object sender, RoutedEventArgs e)
        {
            task.Completed = (bool)CheckboxIsCompleted.IsChecked;

            try
            {
                await TaskRepository.updateTask(task);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private async void EditTask_Click(object sender, RoutedEventArgs e)
        {
            JobEditor jedit = new JobEditor(task.Id, task.Completed, task.Deadline, task.Name, task.Description );
            jedit.ShowDialog();
            //var tc = new TaskControl(task.Id, task.Name, jedit.Deadline, jedit.IsCompleted);
            if (jedit.TaskName == "")
            {
                System.Windows.MessageBox.Show("Please enter a task name");
            }
            else
            {
                //tc.Margin = new Thickness(5);

                Task task = new Task();
                task.Id = Id;
                task.Name = jedit.TaskName;
                task.Description = jedit.Description;
                task.Deadline = jedit.Deadline;
                task.TaskListFk = task_list_id;

                try
                {
                    await TaskRepository.updateTask(task);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error", ex.ToString());
                }

                //getItems();
                //panel.Children.Insert(1, tc);
            }
        }
    }
}
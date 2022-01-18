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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            getItems();
            
        }

        public async void getItems()
        {
            StackPanelChildContainer.Children.Clear();

            List<TaskList> items = await TaskListRepository.getTaskLists();

            //Console.WriteLine(items);
            foreach (TaskList taskList in items)
            {
                BuildAddStackPanel(taskList);
            }
        }

        private async void BuildAddStackPanel(TaskList checks)
        {
            checks.Tasks = await TaskRepository.getTasks(checks.Id);

            StackPanel panel = new StackPanel
            {
                Orientation = Orientation.Vertical,
            };

            ScrollViewer viewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                Margin = new Thickness(5)
            };

            viewer.Content = panel;

            Button btn = new Button
            {
                Name = "ButtonAddTask",
                Content = "Add New Task",
                Margin = new Thickness(2),
                Tag = checks.Id,
           
            };

            btn.Click += new RoutedEventHandler(ButtonAddTask_Click);

            StackPanel topContent = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Width = 150,
            };

            StackPanel nameAndDescription = new StackPanel
            {
                Orientation = Orientation.Vertical,
            };

            TextBlock txt = new TextBlock
            {
                Background = new SolidColorBrush(Color.FromRgb(0x4f, 0x53, 0x6c)),
                Foreground = Brushes.Black,
                Text = checks.Name
            };

            TextBlock description = new TextBlock
            {
                Background = new SolidColorBrush(Color.FromRgb(0x4f, 0x53, 0x6c)),
                Foreground = Brushes.Black,
                Text = checks.Description
            };


            IconButton editButton = new IconButton
            {
                Icon = new Image {
                    Source = new BitmapImage(new Uri("baseline_edit_black_18dp.png", UriKind.Relative)),
                },
                Tag = checks
            };

            editButton.Click += EditButton_Click;

            //IconButton iconButton = new IconButton
            //{
            //    Icon = Image.SourceProperty('')
            //}

            IconButton deleteButton = new IconButton
            {
                Name = "ButtonRemoveList",
                Icon = new Image
                {
                    Source = new BitmapImage(new Uri("baseline_delete_black_18dp.png", UriKind.Relative)),
                },
                Tag = checks
            };

            deleteButton.Click += DeleteButton_Click;

            nameAndDescription.Children.Add(txt);
            nameAndDescription.Children.Add(description);
            topContent.Children.Add(nameAndDescription);
            topContent.Children.Add(editButton);

            if (checks.Tasks.Count == 0)
            {
                topContent.Children.Add(deleteButton);
            }


            panel.Children.Add(topContent);

            foreach (var item in checks.Tasks)
            {
                TaskControl tc = new TaskControl(item, checks.Id, getItems);
                tc.MouseDoubleClick += Tc_MouseDoubleClick;
                if (DateTime.Compare(item.Deadline, DateTime.Now) >= 0)// a je overdue task
                {
                    tc.Background = Brushes.Red;
                }
                tc.Margin = new Thickness(5);
                panel.Children.Add(tc);
            }

            panel.Children.Add(btn);

            //StackPanelChildContainer.Children.Clear();
            StackPanelChildContainer.Children.Add(viewer);

            //ja, vemo da je to hilariously suboptimalna implementacija
            //nareto je blo last minute
            //ampak vsaj to ni vec nas problem :^)

            //hvala zlahtic da si ravno nas izbral
            //god fucking dammit
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TaskList taskList = (TaskList)((Button)sender).Tag;
            ListEditor ledit = new ListEditor(taskList.Name, taskList.Description);
            ledit.ShowDialog();

            if (ledit.ListName != "" || ledit.Description != "")
            {
                taskList.Name = ledit.ListName;
                taskList.Description = ledit.Description;

                try
                {
                    await TaskListRepository.updateTaskList(taskList);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error", ex.ToString());
                }

                getItems();
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TaskList taskList = (TaskList)((Button)sender).Tag;

            try
            {
                await TaskListRepository.deleteTaskList(taskList);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error", ex.ToString());
            }


            getItems();
        }

        private void Tc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TaskControl tc = (TaskControl)sender;

            JobEditor jedit = new JobEditor(0, tc.IsComplete, tc.DueDate, tc.JobName, tc.Description);
            jedit.ShowDialog();

            tc.JobName = jedit.TaskName;
            tc.DueDate = jedit.Deadline;
            tc.IsComplete = jedit.IsCompleted;
        }

        private async void ButtonAddTask_Click(object sender, RoutedEventArgs e)//sadly samo za frontend, na backend ne pusha podatkov
        {
            Button btn = (Button)sender;
            StackPanel panel = (StackPanel)btn.Parent;

            JobEditor jedit = new JobEditor();
            jedit.ShowDialog();
            //var tc = new TaskControl(jedit.TaskName, jedit.Deadline, jedit.IsCompleted);
            if (jedit.TaskName == "")
            {
                System.Windows.MessageBox.Show("Please enter a task name");
            }
            else
            {
                //tc.Margin = new Thickness(5);

                Task task = new Task();
                task.Name = jedit.TaskName;
                task.Description = jedit.Description;
                task.Deadline = jedit.Deadline;
                task.TaskListFk = (int)((Button)sender).Tag;

                try
                {
                    await TaskRepository.addTask(task);
                }catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error", ex.ToString());
                }

                getItems();
                //panel.Children.Insert(1, tc);
            }
        }

        private async void ButtonAddList_Click(object sender, RoutedEventArgs e)
        {
            ListEditor ledit = new ListEditor();
            ledit.ShowDialog();
            if (ledit.ListName == "" || ledit.Description == "")
            {
                System.Windows.MessageBox.Show("Please enter data");
            }
            else
            {
                //seznam.AddTask(new Task(ledit.TaskName, ledit.Deadline, false));
                TaskList task = new TaskList();
                task.Name = ledit.ListName;
                task.Description = ledit.Description;

                try
                {
                    await TaskListRepository.addTaskLists(task);
                } catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error", ex.ToString());
                }


                getItems();
            }
        }

        private async void RefreshData_Click(object sender, RoutedEventArgs e)
        {
            getItems();
        }
    }
}
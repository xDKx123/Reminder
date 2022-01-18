using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder
{
    public class TaskList
    {
        private int id;
        private List<Task> tasks;
        private string name;
        private string description;

        public TaskList()
        {
            Tasks = new List<Task>();
            Name = "";
        }

        public TaskList(int id, string listName) 
        {
            Id = id;
            Name = listName;
        }

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public List<Task> Tasks {
            get => tasks;
            
            set {
                tasks = value;
                OnPropertyChanged("TaskList");
            }
        }

        public string Name {
            get => name;
            
            set {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get => description;

            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public void AddTask(Task job)
        {
            Tasks.Add(job);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged Members
    }
}
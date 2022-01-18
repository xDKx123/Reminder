using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Reminder
{
    public class Task : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string description;
        private bool completed;
        private DateTime deadline;
        private DateTime created_at;
        private int task_list_fk;

        public Task()
        {

        }

        public Task(int id, string name, DateTime deadline, bool completed, DateTime created_at, int task_list_fk)
        {
            Id = id;
            Name = name;
            Deadline = deadline;
            Completed = completed;
            CreatedAt = created_at;
            TaskListFk = task_list_fk;
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

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public bool Completed {
            get => completed;
            set {
                completed = value;
                OnPropertyChanged("Completed");
            }
        }



        public DateTime Deadline {
            get => deadline;
            set {
                deadline = value;
                OnPropertyChanged("Deadline");
            }
        }

        public DateTime CreatedAt
        {
            get => created_at;
            set
            {
                created_at = value;
                OnPropertyChanged("CreatedAt");
            }
        }


        public int TaskListFk
        {
            get => task_list_fk;
            set
            {
                task_list_fk = value;
                OnPropertyChanged("TaskListFk");
            }
        }


        public string Name {
            get => name;
            set {
                name = value;
                OnPropertyChanged("Name");
            }
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
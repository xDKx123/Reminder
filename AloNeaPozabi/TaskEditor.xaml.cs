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
using System.Windows.Shapes;
using System.ComponentModel;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for JobEditor.xaml
    /// </summary>
    public partial class JobEditor : Window, INotifyPropertyChanged
    {
        private int id;
        public JobEditor(int id, bool isdone, DateTime deadline, string name, string description)
        {
            InitializeComponent();
            Id = id;
            DatePickerDueDate.DisplayDate = deadline;
            CheckboxIsCompleted.IsChecked = isdone;
            TextBoxJobName.Text = name;
            TextBoxDescription.Text = description;
        }

        public JobEditor()
        {
            InitializeComponent();
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string TaskName {
            get { return TextBoxJobName.Text; }
            set { TextBoxJobName.Text = value; }
        }

        public bool IsCompleted {
            get { return (bool)CheckboxIsCompleted.IsChecked; }
            set { CheckboxIsCompleted.IsChecked = value; OnPropertyChanged("isCompleted"); }
        }

        public DateTime Deadline {
            get { return DatePickerDueDate.DisplayDate; }
            set { DatePickerDueDate.DisplayDate = value; }
        }

        public string Description
        {
            get { return TextBoxDescription.Text; }
            set { TextBoxDescription.Text = value; }
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

        private void ButtonUpdateTask_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CheckboxIsCompleted_Checked(object sender, RoutedEventArgs e)
        {
            //CheckboxIsCompleted.Content = "checked";

        }

        private void CheckboxIsCompleted_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
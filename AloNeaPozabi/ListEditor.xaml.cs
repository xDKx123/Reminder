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

namespace Reminder
{
    /// <summary>
    /// Interaction logic for ListEditor.xaml
    /// </summary>
    public partial class ListEditor : Window
    {
        public ListEditor()
        {
            InitializeComponent();
        }

        public ListEditor(string name, string descripiton)
        {
            InitializeComponent();
            ListName = name;
            Description = descripiton;
            //Deadline = deadline;
        }

        public string ListName {
            get { return TextBoxListName.Text; }
            set { TextBoxListName.Text = value; }
        }

        public string Description {
            get { return TextBoxDescription.Text; }
            set { TextBoxDescription.Text = value; }
        }

        //public DateTime Deadline {
        //    get { return DatePickerDueDate.DisplayDate; }
        //    set { DatePickerDueDate.DisplayDate = value; }
        //}

        private void ButtonUpdateList_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
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
using TaskHub.Models.events;
using TaskHub.ViewModels;

namespace TaskHub.Views
{

    public delegate void SubmitButtonHandler(Object sender, NewEntryEventArgs e);
    /// <summary>
    /// Interaction logic for NewTaskView.xaml
    /// </summary>
    public partial class NewTaskView : Page,INotifyPropertyChanged
    {
        public event SubmitButtonHandler SubmitButtonPressed;
        public event PropertyChangedEventHandler PropertyChanged;

        public string TaskName { get; set; }
        private string _TaskDescription;

        public string TaskDescription
        {
            get { return _TaskDescription; }
            set { _TaskDescription = value;
                OnPropertyChanged();
            }
        }
        private TextBox _TB;

        public TextBox TB
        {
            get { return _TB; }
            set { _TB = value; OnPropertyChanged(); }
        }


        //List<string> _TaskStatus = new List<string>() { "active", "inactive", "in progress", "finished", "urgent" };

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public NewTaskView()
        {
            InitializeComponent();
            TbTaskName.MaxLength = 50;
            TbDescription.MaxLength = 2000;

        }



        
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            var name = TbTaskName.Text.Trim();
            if (name.Length > 0)
            {
                TaskName = name;
                TaskDescription = TbDescription.Text;
            }
            else
            {
                LblError.Text = "sorry, you have to enter a Valid Name!";
                return;
            }

            SubmitButtonPressed?.Invoke(this, new NewEntryEventArgs(TaskName,TaskDescription,CmbStatus.Text));

            TbTaskName.Clear();
            TbDescription.Clear();
        }

        
    }
}

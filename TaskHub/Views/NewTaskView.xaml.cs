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
    public partial class NewTaskView : Page
    {

        public NewTaskView()
        {
            InitializeComponent();
            TbTaskName.MaxLength = 50;
            TbDescription.MaxLength = 2000;

        }

    }
}

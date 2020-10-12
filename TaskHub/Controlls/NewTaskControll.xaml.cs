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

namespace TaskHub
{
    /// <summary>
    /// Interaction logic for NewTaskControll.xaml
    /// </summary>
    public partial class NewTaskControll : UserControl
    {
        public NewTaskControll()
        {
            InitializeComponent();
        }

        private void TbTaskDescr_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Taskdescription")
            {
                tb.Clear();
                tb.Foreground = Brushes.Black;
            }
            else return;
        }

        private void TbTaskDescr_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "")
            {
                tb.Text = "Taskdescription";
                tb.Foreground = Brushes.LightGray;
            }
            else return;
        }

        private void TbTaskName_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "new Task")
            {
                tb.Clear();
                tb.Foreground = Brushes.Black;
            }
            else return;
        }

        private void TbTaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "")
            {
                tb.Text = "new Task";
                tb.Foreground = Brushes.LightGray;
            }
            else return;
        }
    }
}

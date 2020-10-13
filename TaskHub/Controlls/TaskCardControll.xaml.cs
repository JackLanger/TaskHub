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
    /// Interaction logic for TaskCardControll.xaml
    /// </summary>
    public partial class TaskCardControll : UserControl
    {
        public TaskCardControll()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;

            if (tb.Text == "new Task")
            {
                tb.Clear();
            }
            else return;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;

            if (tb.Text == "")
            {
                tb.Text = "new Task";
            }
            else return;
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;

            if (tb.Text == "description")
            {
                tb.Clear();
            }
            else return;
        }

        private void TextBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;

            if (tb.Text == "")
            {
                tb.Text = "description";
            }
            else return;
        }

    }
}

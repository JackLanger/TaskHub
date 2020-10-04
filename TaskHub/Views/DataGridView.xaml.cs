using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TaskHub.DAL;
using TaskHub.Model;
using TaskHub.Models.events;
using TaskHub.ViewModels;

namespace TaskHub.Views
{
    /// <summary>
    /// Interaction logic for DataGridView.xaml
    /// </summary>
    public partial class DataGridView : Page
    {

        public DataGridView()
        {
            InitializeComponent();

        }

        private void DataGridTemplateColumn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

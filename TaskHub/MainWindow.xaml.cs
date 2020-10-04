using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
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
using TaskHub.Model;
using TaskHub.Models.events;
using TaskHub.ViewModels;
using TaskHub.Views;

namespace TaskHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        DataGridView dataGridView = new DataGridView();
        NewTaskView newTaskView = new NewTaskView();
        DetailView detailView = new DetailView();
        HomeView homeView = new HomeView();

        MainViewModel main = new MainViewModel();


        public MainWindow()
        {

            InitializeComponent();
            MainFrame.Content = dataGridView;


            dataGridView.CmbFilter.ItemsSource = Enum.GetValues(typeof(ActivityCheck));
            dataGridView.CmbFilter.SelectedIndex = 0;
            dataGridView.CmbFilter.SelectionChanged += CmbFilter_SelectionChanged;

            newTaskView.CmbStatus.ItemsSource = Enum.GetValues(typeof(ActivityCheck));
            newTaskView.CmbStatus.SelectedIndex = 0;

            /*-----Subscribe to events------*/
            newTaskView.SubmitButtonPressed += NewTaskView_SubmitButtonPressed;
            dataGridView.TasksList.SelectedCellsChanged += TasksList_SelectedCellsChanged;
            dataGridView.TasksList.MouseDoubleClick += TasksList_MouseDoubleClick;

        }


        private void TasksList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (detailView != null && main.ActiveTask != null)
                MainFrame.Content = detailView;
        }

        private void TasksList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            main.UpdateTaskModel(dataGridView.TasksList.SelectedItem);
            detailView.DataContext = main.ActiveTask;
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            main.FilterData(dataGridView.CmbFilter.SelectedValue.ToString());
            dataGridView.TasksList.ItemsSource = main.TasksList;
        }

        private void NewTaskView_SubmitButtonPressed(object sender, NewEntryEventArgs e) => main.AddNewEntry(e.Name, e.Info, e.Status);

        private void ShowData_Click(object sender, RoutedEventArgs e) => MainFrame.Content = dataGridView ?? new DataGridView();
        void AddData_Click(object sender, RoutedEventArgs e) => MainFrame.Content = newTaskView ?? new NewTaskView();

        private void Home_Click(object sender, RoutedEventArgs e) => MainFrame.Content = homeView ?? new HomeView();
    }
}

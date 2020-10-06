using System;
using System.Windows;
using System.Windows.Controls;
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
        CardView cardView = new CardView();

        MainViewModel main = new MainViewModel();
        TaskCardListControll taskCardListControll = new TaskCardListControll();

        public MainWindow()
        {

            DataContext = main;

            InitializeComponent();

            dataGridView.CmbFilter.ItemsSource = Enum.GetValues(typeof(ActivityCheck));
            dataGridView.CmbFilter.SelectedIndex = 0;
            dataGridView.CmbFilter.SelectionChanged += CmbFilter_SelectionChanged;

            newTaskView.CmbStatus.ItemsSource = Enum.GetValues(typeof(ActivityCheck));
            newTaskView.CmbStatus.SelectedIndex = 0;

            /*-----Subscribe to events------*/
            newTaskView.SubmitButtonPressed += NewTaskView_SubmitButtonPressed;
            dataGridView.TasksList.SelectedCellsChanged += TasksList_SelectedCellsChanged;

        }



        private void TasksList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            main.UpdateTaskModel(dataGridView.TasksList.SelectedItem);
            
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            main.FilterData(dataGridView.CmbFilter.SelectedValue.ToString());
            dataGridView.TasksList.ItemsSource = main.TasksList;
        }

        private void NewTaskView_SubmitButtonPressed(object sender, NewEntryEventArgs e) => main.AddNewEntry(e.Name, e.Info, e.Status);

        private void ShowData_Click(object sender, RoutedEventArgs e) => MainFrame.Content = dataGridView ?? new DataGridView();
        void AddData_Click(object sender, RoutedEventArgs e) => MainFrame.Content = newTaskView ?? new NewTaskView();
        void CardView_Click(object sender, RoutedEventArgs e) => MainFrame.Content = cardView ?? new CardView();
        private void Home_Click(object sender, RoutedEventArgs e) => MainFrame.Content = taskCardListControll ?? new TaskCardListControll();
    }
}

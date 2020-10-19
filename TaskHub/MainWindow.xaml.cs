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
        public MainWindow()
        {
            DataContext = new MainViewModel();

            InitializeComponent();
        }

    }
}

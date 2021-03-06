﻿using System.Windows;
using System.Windows.Controls;


namespace TaskHub
{
    public class DeleteHelper:DependencyObject
    {

        public static readonly DependencyProperty CanDeleteProperty = DependencyProperty.RegisterAttached("CanDelete", typeof(bool),typeof(DeleteHelper),
                                                                                                           new PropertyMetadata (false, OnCanDeleteChanged));

        private static void OnCanDeleteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var delete = d as Button;

            if (delete == null)
                return;

            delete.Click -= Delete_Click;

            if ( (bool)e.NewValue)
                delete.Click += Delete_Click;
        }
        private static void Delete_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public static void SetCanDelete(DependencyObject target, bool value) => target.SetValue(CanDeleteProperty, value);

        public static bool GetCanDelete(DependencyObject target) => (bool)target.GetValue(CanDeleteProperty);

    }
}

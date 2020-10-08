using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskHub.Model;



namespace TaskHub
{
    public class DeleteHelper:DependencyObject
    {
        public static readonly DependencyProperty CanDeleteThisProperty = DependencyProperty.RegisterAttached("CanDelete", typeof(bool), typeof(TaskModel));

        public static void SetCanDelete(DependencyObject target, Boolean value)
        {
            target.SetValue(CanDeleteThisProperty, value);
        }

        public static bool GetCanDelete(DependencyObject target)
        {
            return (bool)target.GetValue(CanDeleteThisProperty);
        }

    }
}

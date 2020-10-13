using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TaskHub.Model;

namespace TaskHub
{
    public class BackgroundHelper:DependencyObject
    {
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.RegisterAttached(
                "IsActive",
                typeof(bool),
                typeof(BackgroundHelper),
                new PropertyMetadata(true)
                );

        public void SetIsActive(DependencyObject target, bool value) => target.SetValue(IsActiveProperty, value);

        public bool GetIsActive(DependencyObject target) => (bool)target.GetValue(IsActiveProperty);
    }
}

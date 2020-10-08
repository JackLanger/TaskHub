using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TaskHub
{
    public class DynamicColor : DependencyObject
    {
        
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty BackGroundColorProperty = DependencyProperty.RegisterAttached("BackgroundColor", typeof(Color), typeof(Button));


        public  void SetValue(DependencyObject dObj, Brushes value)
        {
            dObj.SetValue(BackGroundColorProperty,value);
        }

        public Brush GetValue(DependencyObject dObj)
        {
            return (Brush)dObj.GetValue(BackGroundColorProperty);
        }

        private static void DynamicBackgroundColorChange (DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            var UiButton = dObj as Button;

            UiButton.Background = (UiButton.Content) switch

            {
                ActivityCheck.active => Brushes.Green,
                ActivityCheck.finished => Brushes.Transparent,
                ActivityCheck.urgent => Brushes.Red,
                ActivityCheck.inProgress => Brushes.Yellow,
                ActivityCheck.inactive => Brushes.Blue,
                
                _ => Brushes.Transparent,
            };
        }

    }
}

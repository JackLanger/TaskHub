using System;
using System.Windows;
using System.Windows.Controls;

namespace TaskHub
{
    public class PasswordHelper : DependencyObject
    {

        public static DependencyProperty IsMatchProperty = DependencyProperty.RegisterAttached("IsMatch", typeof(bool),
                                                                                                typeof(PasswordHelper),
                                                                                                new PropertyMetadata(false, OnIsMatchChanged));


        public static DependencyProperty HasTextProperty = DependencyProperty.RegisterAttached("HasText", typeof(bool),
                                                                                                typeof(PasswordHelper), 
                                                                                                new PropertyMetadata(false, OnHasTextChanged));


        private void SetIsMatch(DependencyObject target, bool value) => target.SetValue(IsMatchProperty, value);
        private bool GetIsMatch(DependencyObject target) => (bool)target.GetValue(IsMatchProperty);

        private void SetHasText(DependencyObject target, bool value) => target.SetValue(HasTextProperty, value);
        private bool GetHasText(DependencyObject target) => (bool)target.GetValue(HasTextProperty);

        private static void OnIsMatchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //do something
        }
        private static void OnHasTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            // do something
        }

    }
}

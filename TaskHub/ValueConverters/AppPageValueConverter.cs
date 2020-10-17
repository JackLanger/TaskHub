using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using TaskHub.Views;

namespace TaskHub

{
    public class AppPageValueConverter :BaseValueConverter<AppPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object output = ((ApplicationPage)value) switch
            {
                ApplicationPage.Home => new TaskCardListControl(),
                ApplicationPage.DataGrid => new DataGridView(),

                _ => throw new NotImplementedException("page not found"),
            };
            return output;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

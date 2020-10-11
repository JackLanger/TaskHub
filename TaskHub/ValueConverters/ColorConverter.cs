using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TaskHub
{
    public class ColorConverter : BaseValueConverter<ColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converter = new BrushConverter();
            var background = (string)value switch
            {
                nameof(ActivityCheck.active) => converter.ConvertFrom("#34e7e4"),
                nameof(ActivityCheck.finished) => converter.ConvertFrom("#05c46b"),
                nameof(ActivityCheck.inactive) => converter.ConvertFrom("#d2dae2"),
                nameof(ActivityCheck.inProgress) => converter.ConvertFrom("#ffa801"),
                nameof(ActivityCheck.urgent) => converter.ConvertFrom("#ff3f34"),
                _ => converter.ConvertFrom("#fff")
            };
            return background;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

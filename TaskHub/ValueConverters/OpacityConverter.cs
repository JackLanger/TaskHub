﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHub
{
    public class OpacityConverter : BaseValueConverter<OpacityConverter>
    {
        public override object Convert( object value, Type targetType, 
                                        object parameter, CultureInfo culture) 
            => (bool)value ? 1 : 0.3;

        public override object ConvertBack( object value, Type targetType, 
                                            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

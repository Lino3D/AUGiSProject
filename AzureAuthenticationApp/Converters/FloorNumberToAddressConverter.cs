﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace AzureAuthenticationApp.Converters
{
    public class FloorNumberToAddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int)) return null;
            if ((int)value != 0)
                return "http://samorzad.mini.pw.edu.pl/plan2/images/Pietro" + (int)value + "/{z}/{x}/{y}.png";
            return "http://samorzad.mini.pw.edu.pl/plan2/images/Parter" + "/{z}/{x}/{y}.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
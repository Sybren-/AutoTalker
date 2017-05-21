using FunProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace FunProject.Converters
{
    [ValueConversion(typeof(ObservableCollection<TextColor>), typeof(string))]
    class TextColorCollectionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var textColors = (ObservableCollection<TextColor>)values[0];

            return string.Join(", ", textColors.Select(color => color.Name));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

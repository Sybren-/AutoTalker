using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FunProject.Converters
{
    [ValueConversion(typeof(ObservableCollection<TextEffect>), typeof(string))]
    class TextEffectCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var textEffects = (ObservableCollection<TextEffect>)value;

            var sb = new StringBuilder();
            foreach (var textEffect in textEffects)
            {
                sb.AppendLine(textEffect.Name);
            }

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

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
    class TextEffectCollectionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var textEffects = (ObservableCollection<TextEffect>)values[0];

            return string.Join(", ", textEffects.Select(effect => effect.Name));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

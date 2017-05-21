using FunProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunProject
{
    internal static class TextEffectsProcessor
    {
        internal static string ProcessMessageTextColor(string message, TextColor textColor, ObservableCollection<TextEffect> textEffects)
        {
            var colorCode = textColor?.Code ?? string.Empty;
            var textEffectCodesString = string.Concat(textEffects.Select(effect => effect.Code));

            return string.Format("{0}{1}{2}", colorCode, textEffectCodesString, message);
        }
    }
}

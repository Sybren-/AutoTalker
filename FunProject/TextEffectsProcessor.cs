using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunProject
{
    internal static class TextEffectsProcessor
    {
        internal static string ProcessMessageTextColor(string message, string colorCode, string effectCode)
        {
            return string.Format("{0}{1}{2}", colorCode, effectCode, message);
        }
    }
}

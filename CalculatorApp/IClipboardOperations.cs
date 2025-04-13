using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public interface IClipboardOperations
    {
        string GetDisplayText();
        void SetDisplayText(string text);

        void Cut(); 
        void Copy(); 
        void Paste(); 

        void DigitGrouping(string language);
    }

}

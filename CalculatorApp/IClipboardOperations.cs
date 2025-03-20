using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public interface IClipboardOperations
    {
        // Returnează textul curent ce poate fi folosit pentru Copy sau Cut
        string GetDisplayText();
        // Setează displayul pe baza unui text (de exemplu, după Paste)
        void SetDisplayText(string text);

        // Operații pentru Cut, Copy și Paste
        void Cut();   // Copiază textul curent și apoi îl golește
        void Copy();  // Copiază textul curent
        void Paste(); // Inserează textul copiat în display

        // Operație pentru digit grouping – de exemplu, adaugă separatori de mii
        void DigitGrouping(string language);
    }

}

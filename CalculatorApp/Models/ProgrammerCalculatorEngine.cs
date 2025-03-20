using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Models
{
    public class ProgrammerCalculatorEngine
    {
        private long _previousValue;
        private char _pendingOperator;
        private bool _isNewEntry;
        private string _currentEntry;

        public ProgrammerCalculatorEngine()
        {
            ClearAll();
        }

        // Proprietate pentru afișajul curent (în format hex)
        public string CurrentEntry => _currentEntry;

        // Permite setarea directă a intrării curente (de exemplu, dintr-o selecție din istoric)
        public void SetCurrentEntry(string entry)
        {
            _currentEntry = entry;
            _isNewEntry = true;
        }

        // Adaugă o cifră sau literă (0-9, A-F)
        public void InputDigit(string digit)
        {
            if (_isNewEntry || _currentEntry == "0")
            {
                _currentEntry = digit;
                _isNewEntry = false;
            }
            else
            {
                _currentEntry += digit;
            }
        }

        // Setează operatorul (+, -, *, /, %). Dacă se introduce un nou operator, se efectuează calculul curent.
        public void SetOperator(char op)
        {
            if (!_isNewEntry)
            {
                Calculate();
            }
            _pendingOperator = op;
            _previousValue = ParseHex(_currentEntry);
            _isNewEntry = true;
        }

        // Efectuează calculul folosind operatorul curent
        public void Calculate()
        {
            long currentNumber = ParseHex(_currentEntry);
            long result = currentNumber;
            if (_pendingOperator != '\0')
            {
                switch (_pendingOperator)
                {
                    case '+':
                        result = _previousValue + currentNumber;
                        break;
                    case '-':
                        result = _previousValue - currentNumber;
                        break;
                    case '*':
                        result = _previousValue * currentNumber;
                        break;
                    case '/':
                        if (currentNumber == 0)
                            throw new DivideByZeroException("Division by zero is not allowed.");
                        result = _previousValue / currentNumber;
                        break;
                    case '%':
                        result = _previousValue % currentNumber;
                        break;
                }
                _currentEntry = FormatHex(result);
                _pendingOperator = '\0';
                _previousValue = result;
                _isNewEntry = true;
            }
        }

        // Șterge doar intrarea curentă (CE)
        public void ClearEntry()
        {
            _currentEntry = "0";
            _isNewEntry = true;
        }

        // Resetează complet calculatorul (C)
        public void ClearAll()
        {
            _currentEntry = "0";
            _previousValue = 0;
            _pendingOperator = '\0';
            _isNewEntry = true;
        }

        // Șterge ultimul caracter (Backspace)
        public void Backspace()
        {
            if (!_isNewEntry && _currentEntry.Length > 0)
            {
                _currentEntry = _currentEntry.Substring(0, _currentEntry.Length - 1);
                if (string.IsNullOrEmpty(_currentEntry) || _currentEntry == "-")
                {
                    _currentEntry = "0";
                    _isNewEntry = true;
                }
            }
        }

        // Schimbă semnul (±)
        public void ChangeSign()
        {
            if (_currentEntry.StartsWith("-"))
                _currentEntry = _currentEntry.Substring(1);
            else if (_currentEntry != "0")
                _currentEntry = "-" + _currentEntry;
        }

        // Metodă ajutătoare pentru a parsa intrarea curentă (format hex) într-un număr întreg
        private long ParseHex(string entry)
        {
            bool isNegative = entry.StartsWith("-");
            string valueString = isNegative ? entry.Substring(1) : entry;
            long value = 0;
            try
            {
                // Convertim string-ul în număr, folosind baza 16
                value = Convert.ToInt64(valueString, 16);
            }
            catch
            {
                value = 0;
            }
            return isNegative ? -value : value;
        }

        // Formatează un număr întreg în șir hexazecimal (cu litere majuscule)
        private string FormatHex(long value)
        {
            if (value < 0)
                return "-" + Convert.ToString(-value, 16).ToUpper();
            else
                return Convert.ToString(value, 16).ToUpper();
        }

        // Metode de conversie pentru a obține reprezentarea în diferite baze
        public string ToHex()
        {
            long value = ParseHex(_currentEntry);
            return FormatHex(value);
        }

        public string ToDec()
        {
            long value = ParseHex(_currentEntry);
            return value.ToString();
        }

        public string ToOct()
        {
            long value = ParseHex(_currentEntry);
            return Convert.ToString(value, 8);
        }

        public string ToBin()
        {
            long value = ParseHex(_currentEntry);
            return Convert.ToString(value, 2);
        }
    }
}
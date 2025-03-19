using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Models
{
    public class CalculatorEngine
    {
        private double _previousValue;
        private char _pendingOperator;
        private bool _isNewEntry;

        // Store the current input as a string for manipulation (e.g., backspace)
        private string _currentEntry;

        public CalculatorEngine()
        {
            ClearAll();
        }

        // Property for display output
        public string CurrentEntry => _currentEntry;

        // Add a digit (e.g., "1", "2", etc.)
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

        // Add a decimal separator
        public void InputDecimal()
        {
            if (_isNewEntry)
            {
                _currentEntry = "0.";
                _isNewEntry = false;
            }
            else if (!_currentEntry.Contains("."))
            {
                _currentEntry += ".";
            }
        }

        // Set the operator (+, -, *, /, %)
        public void SetOperator(char op)
        {
            if (!_isNewEntry)
            {
                Calculate(); // If a new operator is entered, perform the current calculation
            }
            _pendingOperator = op;
            _previousValue = double.Parse(_currentEntry);
            _isNewEntry = true;
        }

        // Perform the current calculation
        public void Calculate()
        {
            double currentNumber = double.Parse(_currentEntry);
            double result = currentNumber;
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
                _currentEntry = result.ToString();
                _pendingOperator = '\0';
                _previousValue = result;
                _isNewEntry = true;
            }
        }

        // Clear only the current entry (CE)
        public void ClearEntry()
        {
            _currentEntry = "0";
            _isNewEntry = true;
        }

        // Clear the entire operation (C)
        public void ClearAll()
        {
            _currentEntry = "0";
            _previousValue = 0;
            _pendingOperator = '\0';
            _isNewEntry = true;
        }

        // Backspace – remove the last character
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

        // Change the sign (±)
        public void ChangeSign()
        {
            if (_currentEntry.StartsWith("-"))
                _currentEntry = _currentEntry.Substring(1);
            else if (_currentEntry != "0")
                _currentEntry = "-" + _currentEntry;
        }

        // Square the number (x²)
        public void Square()
        {
            double number = double.Parse(_currentEntry);
            number *= number;
            _currentEntry = number.ToString();
            _isNewEntry = true;
        }

        // Calculate the square root (√x)
        public void SquareRoot()
        {
            double number = double.Parse(_currentEntry);
            if (number < 0)
                throw new Exception("Cannot compute the square root of a negative number.");
            number = Math.Sqrt(number);
            _currentEntry = number.ToString();
            _isNewEntry = true;
        }

        // Calculate the inverse (1/x)
        public void Inverse()
        {
            double number = double.Parse(_currentEntry);
            if (number == 0)
                throw new DivideByZeroException("Division by zero is not allowed.");
            number = 1 / number;
            _currentEntry = number.ToString();
            _isNewEntry = true;
        }
    }
}

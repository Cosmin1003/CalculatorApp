using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using CalculatorApp.Models;


namespace CalculatorApp.ViewModels
{
    public class StandardModeViewModel : INotifyPropertyChanged
    {
        private CalculatorEngine _calculator;
        private string _display;

        public string Display
        {
            get => _display;
            set
            {
                _display = value;
                OnPropertyChanged(nameof(Display));
            }
        }

        // Comenzi pentru diferite acțiuni
        public ICommand DigitCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand EqualCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand ClearEntryCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand DecimalCommand { get; }
        public ICommand ChangeSignCommand { get; }
        public ICommand SquareCommand { get; }
        public ICommand SquareRootCommand { get; }
        public ICommand InverseCommand { get; }

        public StandardModeViewModel()
        {
            _calculator = new CalculatorEngine();
            Display = _calculator.CurrentEntry;

            DigitCommand = new RelayCommand(param => AppendDigit(param.ToString()));
            DecimalCommand = new RelayCommand(param => InputDecimal());
            OperatorCommand = new RelayCommand(param => SetOperator(param.ToString()[0]));
            EqualCommand = new RelayCommand(param => CalculateResult());
            ClearCommand = new RelayCommand(param => ClearAll());
            ClearEntryCommand = new RelayCommand(param => ClearEntry());
            BackspaceCommand = new RelayCommand(param => Backspace());
            ChangeSignCommand = new RelayCommand(param => ChangeSign());
            SquareCommand = new RelayCommand(param => Square());
            SquareRootCommand = new RelayCommand(param => SquareRoot());
            InverseCommand = new RelayCommand(param => Inverse());
        }

        private void AppendDigit(string digit)
        {
            try
            {
                _calculator.InputDigit(digit);
                Display = _calculator.CurrentEntry;
            }
            catch
            {
                Display = "Error";
            }
        }

        private void InputDecimal()
        {
            try
            {
                _calculator.InputDecimal();
                Display = _calculator.CurrentEntry;
            }
            catch
            {
                Display = "Error";
            }
        }

        private void SetOperator(char op)
        {
            try
            {
                _calculator.SetOperator(op);
                Display = _calculator.CurrentEntry;
            }
            catch
            {
                Display = "Error";
            }
        }

        private void CalculateResult()
        {
            try
            {
                _calculator.Calculate();
                Display = _calculator.CurrentEntry;
            }
            catch
            {
                Display = "Error";
            }
        }

        private void ClearAll()
        {
            _calculator.ClearAll();
            Display = _calculator.CurrentEntry;
        }

        private void ClearEntry()
        {
            _calculator.ClearEntry();
            Display = _calculator.CurrentEntry;
        }

        private void Backspace()
        {
            _calculator.Backspace();
            Display = _calculator.CurrentEntry;
        }

        private void ChangeSign()
        {
            _calculator.ChangeSign();
            Display = _calculator.CurrentEntry;
        }

        private void Square()
        {
            try
            {
                _calculator.Square();
                Display = _calculator.CurrentEntry;
            }
            catch
            {
                Display = "Error";
            }
        }

        private void SquareRoot()
        {
            try
            {
                _calculator.SquareRoot();
                Display = _calculator.CurrentEntry;
            }
            catch
            {
                Display = "Error";
            }
        }

        private void Inverse()
        {
            try
            {
                _calculator.Inverse();
                Display = _calculator.CurrentEntry;
            }
            catch
            {
                Display = "Error";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

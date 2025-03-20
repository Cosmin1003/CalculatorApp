using CalculatorApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorApp.Views;
using System.ComponentModel;
using System.Windows.Input;
using CalculatorApp.Models;

namespace CalculatorApp.ViewModels
{
    public class ProgrammerModeViewModel : INotifyPropertyChanged
    {
        private object _currentInterface;
        public object CurrentInterface
        {
            get => _currentInterface;
            set
            {
                _currentInterface = value;
                OnPropertyChanged(nameof(CurrentInterface));
            }
        }


        private ProgrammerCalculatorEngine _calculator;
        private string _display;
        public string Display
        {
            get => _display;
            set
            {
                _display = value;
                OnPropertyChanged(nameof(Display));
                UpdateConversions();
            }
        }

        // Proprietăți pentru afișarea conversiilor
        private string _hexValue;
        public string HexValue
        {
            get => _hexValue;
            set { _hexValue = value; OnPropertyChanged(nameof(HexValue)); }
        }

        private string _decValue;
        public string DecValue
        {
            get => _decValue;
            set { _decValue = value; OnPropertyChanged(nameof(DecValue)); }
        }

        private string _octValue;
        public string OctValue
        {
            get => _octValue;
            set { _octValue = value; OnPropertyChanged(nameof(OctValue)); }
        }

        private string _binValue;
        public string BinValue
        {
            get => _binValue;
            set { _binValue = value; OnPropertyChanged(nameof(BinValue)); }
        }

        // Comenzi pentru operații și cifre
        public ICommand DigitCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand EqualCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand ClearEntryCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand ChangeSignCommand { get; }


        public ICommand ShowProgrammerModeButtonsCommand { get; }
        public ICommand ShowProgrammerModeBinaryCommand { get; }

        public ProgrammerModeViewModel()
        {
            _calculator = new ProgrammerCalculatorEngine();
            Display = _calculator.CurrentEntry;

            DigitCommand = new RelayCommand(param => AppendDigit(param.ToString()));
            OperatorCommand = new RelayCommand(param => SetOperator(param.ToString()[0]));
            EqualCommand = new RelayCommand(param => CalculateResult());
            ClearCommand = new RelayCommand(param => ClearAll());
            ClearEntryCommand = new RelayCommand(param => ClearEntry());
            BackspaceCommand = new RelayCommand(param => Backspace());
            ChangeSignCommand = new RelayCommand(param => ChangeSign());

            UpdateConversions();

            // Setăm interfața implicită:
            CurrentInterface = new ProgrammerModeButtons();

            ShowProgrammerModeButtonsCommand = new RelayCommand(_ => CurrentInterface = new ProgrammerModeButtons());
            ShowProgrammerModeBinaryCommand = new RelayCommand(_ => CurrentInterface = new ProgrammerModeBinary());
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

        // Actualizează proprietățile de conversie pe baza valorii curente
        private void UpdateConversions()
        {
            HexValue = _calculator.ToHex();
            DecValue = _calculator.ToDec();
            OctValue = _calculator.ToOct();
            BinValue = _calculator.ToBin();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

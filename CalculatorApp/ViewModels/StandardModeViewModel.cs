using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using CalculatorApp.Models;
using System.Collections.ObjectModel;


namespace CalculatorApp.ViewModels
{
    public class StandardModeViewModel : INotifyPropertyChanged, IClipboardOperations
    {
        private CalculatorEngine _calculator;
        private string _display;
        private string _language;

        public string Display
        {
            get => _display;
            set
            {
                if (double.TryParse(value, out double number))
                {
                    _display = FormatNumberWithGrouping(number, _language);
                }
                else
                {
                    _display = value;
                }
                OnPropertyChanged(nameof(Display));
            }
        }

        // Commands for standard calculations
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


        // Commands for memory operations
        public ICommand MemoryClearCommand { get; }
        public ICommand MemoryRecallCommand { get; }
        public ICommand MemoryStoreCommand { get; }
        public ICommand MemoryAddCommand { get; }
        public ICommand MemorySubtractCommand { get; }
        public ICommand ToggleMemoryListCommand { get; }
        public ICommand SelectMemoryItemCommand { get; }


        public ObservableCollection<double> MemoryItems { get; } = new ObservableCollection<double>();

        private bool _isMemoryListVisible;
        public bool IsMemoryListVisible
        {
            get => _isMemoryListVisible;
            set
            {
                _isMemoryListVisible = value;
                OnPropertyChanged(nameof(IsMemoryListVisible));
            }
        }

        public StandardModeViewModel()
        {
            _calculator = new CalculatorEngine();
            _language = Properties.Settings.Default.GroupingLanguage;
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

            MemoryClearCommand = new RelayCommand(param => { MemoryClear(); RefreshMemoryItems(); });
            MemoryRecallCommand = new RelayCommand(param => { MemoryRecall(); RefreshMemoryItems(); UpdateDisplay(); });
            MemoryStoreCommand = new RelayCommand(param => { MemoryStore(); RefreshMemoryItems(); });
            MemoryAddCommand = new RelayCommand(param => { MemoryAdd(); RefreshMemoryItems(); });
            MemorySubtractCommand = new RelayCommand(param => { MemorySubtract(); RefreshMemoryItems(); });
            ToggleMemoryListCommand = new RelayCommand(param => ToggleMemoryList());
            SelectMemoryItemCommand = new RelayCommand(param => SelectMemoryItem(param));

            IsMemoryListVisible = false;
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

        // Methods for memory operations
        private void MemoryClear()
        {
            _calculator.MemoryClear();
        }

        private void MemoryRecall()
        {
            _calculator.MemoryRecall();
        }

        private void MemoryStore()
        {
            _calculator.MemoryStore();
        }

        private void MemoryAdd()
        {
            _calculator.MemoryAdd();
        }

        private void MemorySubtract()
        {
            _calculator.MemorySubtract();
        }

        private void UpdateDisplay()
        {
            Display = _calculator.CurrentEntry;
        }

        private void RefreshMemoryItems()
        {
            MemoryItems.Clear();
            foreach (var item in _calculator.MemoryList.Reverse())
            {
                MemoryItems.Add(item);
            }
        }

        private void ToggleMemoryList()
        {
            IsMemoryListVisible = !IsMemoryListVisible;
        }

        private void SelectMemoryItem(object param)
        {
            if (param is double value)
            {
                _calculator.SetCurrentEntry(value.ToString());
                UpdateDisplay();
            }
        }



        public string GetDisplayText() => Display;

        public void SetDisplayText(string text)
        {
            Display = text;
        }

        public void Cut()
        {
            CustomClipboard.Text = Display;
            _calculator.ClearAll();
            Display = _calculator.CurrentEntry;
        }

        public void Copy()
        {
            CustomClipboard.Text = Display;
        }

        public void Paste()
        {
            string pasteText = CustomClipboard.Text;

            if (Display == "0")
            {
                if (pasteText.StartsWith("0") && !pasteText.StartsWith("0."))
                {
                    pasteText = pasteText.TrimStart('0');

                    if (string.IsNullOrEmpty(pasteText))
                    {
                        pasteText = "0";
                    }
                }

                Display = pasteText;
            }
            else
            {
                Display += pasteText;
            }
            _calculator.SetCurrentEntry(Display);
        }

        public void DigitGrouping(string language)
        {
            _language = language;
            Properties.Settings.Default.GroupingLanguage = language;
            Properties.Settings.Default.Save();

            if (double.TryParse(_calculator.CurrentEntry, out double number))
            {
                Display = FormatNumberWithGrouping(number, language);
            }
            else
            {
                Display = _calculator.CurrentEntry;
            }
        }

        private string FormatNumberWithGrouping(double number, string language)
        {
            string[] parts = number.ToString().Split('.');
            string integerPart = parts[0];
            string fractionalPart = parts.Length > 1 ? parts[1] : string.Empty;

            string thousandSeparator, decimalSeparator;
            switch (language?.ToLower())
            {
                case "ro":
                    thousandSeparator = ".";
                    decimalSeparator = ",";
                    break;
                case "en":
                    thousandSeparator = ",";
                    decimalSeparator = ".";
                    break;
                default:
                    thousandSeparator = ",";
                    decimalSeparator = ".";
                    break;
            }

            StringBuilder sb = new StringBuilder();
            int count = 0;
            for (int i = integerPart.Length - 1; i >= 0; i--)
            {
                sb.Insert(0, integerPart[i]);
                count++;
                if (count % 3 == 0 && i > 0 && integerPart[i - 1] != '-')
                {
                    sb.Insert(0, thousandSeparator);
                }
            }
            return fractionalPart != string.Empty
                ? $"{sb.ToString()}{decimalSeparator}{fractionalPart}"
                : sb.ToString();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorApp.Views;
using CalculatorApp;
using System.Windows;

namespace CalculatorApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentViewModel;
        private string _buttonText;

        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        public ICommand SwitchInterfaceCommand { get; }

        // Comenzi pentru operații de clipboard și digit grouping
        public ICommand CutCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand DigitGroupingCommand { get; }

        public MainViewModel()
        {
            CutCommand = new RelayCommand(param => ExecuteClipboardOperation(op => op.Cut()));
            CopyCommand = new RelayCommand(param => ExecuteClipboardOperation(op => op.Copy()));
            PasteCommand = new RelayCommand(param => ExecuteClipboardOperation(op => op.Paste()));
            DigitGroupingCommand = new RelayCommand(param =>
            {
                if (param is string language)
                {
                    ExecuteClipboardOperation(op => op.DigitGrouping(language));
                }
            });


            // Inițial, afișăm prima interfață
            CurrentViewModel = new StandardMode();
            ButtonText = "Standard Mode"; // Textul butonului inițial
            SwitchInterfaceCommand = new RelayCommand(param => SwitchInterface());
        }

        private void ExecuteClipboardOperation(Action<IClipboardOperations> operation)
        {
            if (CurrentViewModel is FrameworkElement view && view.DataContext is IClipboardOperations clipboardViewModel)
            {
                System.Diagnostics.Debug.WriteLine("Comandă clipboard executată.");
                operation(clipboardViewModel);
            }
        }

        private void SwitchInterface()
        {
            // Comută între ViewModel1 și ViewModel2
            if (CurrentViewModel is StandardMode)
            {
                CurrentViewModel = new ProgrammerMode();
                ButtonText = "Programmer Mode"; // Schimbă textul butonului
            }
            else
            {
                CurrentViewModel = new StandardMode();
                ButtonText = "Standard Mode"; // Schimbă textul butonului
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

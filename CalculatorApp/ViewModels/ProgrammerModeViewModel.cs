using CalculatorApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorApp.Views;
using System.ComponentModel;
using System.Windows.Input;

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

        public ICommand ShowProgrammerModeButtonsCommand { get; }
        public ICommand ShowProgrammerModeBinaryCommand { get; }

        public ProgrammerModeViewModel()
        {
            // Setăm interfața implicită:
            CurrentInterface = new ProgrammerModeButtons();

            ShowProgrammerModeButtonsCommand = new RelayCommand(_ => CurrentInterface = new ProgrammerModeButtons());
            ShowProgrammerModeBinaryCommand = new RelayCommand(_ => CurrentInterface = new ProgrammerModeBinary());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

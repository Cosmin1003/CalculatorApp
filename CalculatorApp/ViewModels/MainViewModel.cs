using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorApp.Views;

namespace CalculatorApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand SchimbaInterfataCommand { get; }

        public MainViewModel()
        {
            // Inițial, afișăm prima interfață
            CurrentViewModel = new View1();
            SchimbaInterfataCommand = new RelayCommand(param => SchimbaInterfata());
        }

        private void SchimbaInterfata()
        {
            // Comută între ViewModel1 și ViewModel2
            if (CurrentViewModel is View1)
                CurrentViewModel = new View2();
            else
                CurrentViewModel = new View1();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

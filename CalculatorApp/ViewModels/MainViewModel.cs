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

        public ICommand SwitchInterfaceCommand { get; }

        public MainViewModel()
        {
            // Inițial, afișăm prima interfață
            CurrentViewModel = new StandardMode();
            SwitchInterfaceCommand = new RelayCommand(param => SwitchInterface());
        }

        private void SwitchInterface()
        {
            // Comută între ViewModel1 și ViewModel2
            if (CurrentViewModel is StandardMode)
                CurrentViewModel = new ProgrammerMode();
            else
                CurrentViewModel = new StandardMode();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

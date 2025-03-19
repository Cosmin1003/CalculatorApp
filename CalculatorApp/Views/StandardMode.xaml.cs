using CalculatorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CalculatorApp.Views
{
    /// <summary>
    /// Interaction logic for View1.xaml
    /// </summary>
    public partial class StandardMode : UserControl
    {
        public StandardMode()
        {
            InitializeComponent();
            DataContext = new StandardModeViewModel();
            Loaded += (s, e) => this.Focus();
            this.PreviewKeyDown += StandardMode_PreviewKeyDown;
        }

        private void StandardMode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.DataContext is StandardModeViewModel viewModel && viewModel.EqualCommand.CanExecute(null))
                {
                    viewModel.EqualCommand.Execute(null);
                    e.Handled = true;
                }

                this.Focus();
            }
        }
    }
}

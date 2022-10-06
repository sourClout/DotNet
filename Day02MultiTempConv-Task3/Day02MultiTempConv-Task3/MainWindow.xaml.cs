using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Day02MultiTempConv_Task3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // point in time just before the gui displays to the user
            InitializeComponent();
           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // call recaluclate right at the start since input starts at 10
            recalculate();
        }

        private void TbxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            // any time text could be changed need to recalc.
            recalculate();
        }


      

        private void TbxInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // anything that is not a number, dot, or space is not allowed
            // regex --> does it have anything that is NOT a digit a dot or a minus
            // we set the e.Handled to either true or false depending on if we want to accept it or not
            // if it has something it shouldnt e.Handled is set to true --> meaning we prevent further processing of info --> means IGNORES user input
            Regex regex = new Regex(@"[^0-9\.-]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AnyRadioButton_Click(object sender, RoutedEventArgs e)
        {
            // any time possibility radio buttons got changed need to recalc. 
            recalculate();
        }

        //The goal is that no matter what input measurement is selected, the first step will be to convert anything to celcius.
        private void recalculate()
        {
            // Do nothing is Xaml hasnt loaded yet. If it has then we need to recalculate.
            if (!this.IsLoaded) return;
            // 1. parse the input (use try parse instead of throw and exception --> improves UI)
            // Always use double instead of float --> better precision 
            if (!double.TryParse(TbxInput.Text, out double inVal))
            {
                // If a number is NOT entered in input, output will read "ERROR"
                TbxOutput.Text = "Error";
                return;
            }
            // 2. Convert to celsius
            double cel;
            if (RbnInCel.IsChecked == true)
            {
                cel = inVal;
            }
            else if (RbnInFah.IsChecked == true)
            {
                cel = (inVal - 32) * 5 / 9;
            }
            else if (RbnInKel.IsChecked == true)
            {
                cel = inVal - 273.15;
            }
            else
            {
                // this should never happen
                MessageBox.Show(this, "Invalid control flow", "Internal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // 3. convert our value in celcius to the selected output
            double outVal;
            // Will show the unit in the output along with temp.
            String unit;

            if (RbnOutCel.IsChecked == true)
            {
                outVal = cel;
                unit = "C";
            }
            else if (RbnOutFah.IsChecked == true)
            {
                outVal = cel * 9 / 5 + 32;
                unit = "F";
            }
            else if (RbnOutKel.IsChecked == true)
            {
                outVal = cel + 273.15;
                unit = "K";
            }
            else
            {
                // this should never happen
                MessageBox.Show(this, "Invalid control flow", "Internal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // 4. Display the result in the output textbox
            // formatting the value with 1 decimal point
            TbxOutput.Text = $"{outVal:0.0}{unit}";



        }

        
    }
}

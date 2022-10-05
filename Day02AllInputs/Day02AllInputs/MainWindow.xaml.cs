using System;
using System.Collections.Generic;
using System.IO;
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

namespace Day02AllInputs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string name = TbxName.Text;
            if (name == "")
            {
                MessageBox.Show(this, "Name must not be empty", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // 1st way --> with LinQ
            // Gives a collection of all radio buttons inside of grid --> asking which of them isChecked

            //var checkedButton = GridMain.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true && r.GroupName == "RbnAge" );
            // FIXME: If checkedButton -- null show error
            // to test which button is checked
            //Console.WriteLine("Checked rb: " + checkedButton.Content);
            //string age = checkedButton.Content;

            // 2nd way --> Longer but no LinQ
            string age = "";
            if (RbnAgeBelow18.IsChecked == true)
            {
                age = "Below 18";
            }
            else if (RbnAge18to35.IsChecked == true)
            {
                age = "18 to 35";
            }
            else if (RbnAge36Plus.IsChecked == true)
            {
                age = "36 and up";
            }
            else
            {
                // Internal error
                MessageBox.Show(this, "Error reading radio button state", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Kyle Solution
            // List<CheckBox> CheckBoxes = new List<CheckBox> { CbxPetsCats, CbxPetsDogs, CbxPetsOther };
            //string pets = string.Join(",", CheckBoxes.Where(Ckb => Ckb.IsChecked == true).Select(cb => cb.Content));

            // Create List of pets, check which checkboxes are selected, add to string list for every one checked.  with Join seperator ",".
            List<string> petsList = new List<string>();
            if (CbxPetsCats.IsChecked == true)
            {
                petsList.Add("Cats");
            }
            if (CbxPetsDogs.IsChecked == true)
            {
                petsList.Add("Dogs");
            }
            if (CbxPetsOther.IsChecked == true)
            {
                petsList.Add("Other");
            }
            // takes a list of an array of strings and puts a seperator between them --> putting a seperator in between the elements of an array.
            string pets = string.Join(",", petsList);

            // CONTINENT

            string continent = ComboContinent.Text;
            //Console.WriteLine("Continent: " + continent);

            // IF isSelected is replaced with entry option
            //if (ComboContinent.SelectedIndex == 0)
            //{
            //    MessageBox.Show(this, "You must select a continent", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            // IF isSelected is removed --> conditional call on the selected value
            // IF the selected value is NOT NULL --> THEN we call toString on it (thats the question mark"?")
            // If the selected value is null and you call tostring on it you get Null Pointer exception (which will crash program)
            // Basically avoid an "if" statement on the first line
            //string continent2 = ComboContinent.SelectedValue?.ToString(); // conditional call
            //if (continent == null)
            //{
            //    MessageBox.Show(this, "Please select a continent", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            // Temperature and Binding
            double tempC = SliderTempC.Value;

            // write to file (append all text) --> with "using" --> similar to "import" in java
            // MUST add a new line to end of append because not added automatically (line + "\n");
            string line = $"{name}; {age}; {pets}; {continent}; {tempC}";
            File.AppendAllText(@"..\..\output.txt", line + "\n");
            MessageBox.Show(this, "Data appended to file", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            //TODO: reset all imputs after successful writing data to file
            

        }
    }
}

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

namespace Day04ListGridViewPeople
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Auto Private --> lowercase in private
        // this is where the data storage is for listview
        List<Person> peopleList = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            LvPeople.ItemsSource = peopleList; // itemSource is to change the source of the data
        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (!ArePersonInputsValid()) return;
            string name = Tbx_Name.Text;
            int.TryParse(Tbx_Age.Text, out int age);
            peopleList.Add(new Person(name,age));
            LvPeople.Items.Refresh(); // tell ListView data has changed --> when performing operations on "itemSource", you just use "Items"
            ResetFields();

        }

        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            Person currSelPer = LvPeople.SelectedItem as Person;
            if (currSelPer == null) return;
            if (!ArePersonInputsValid()) return;
            string name = Tbx_Name.Text;
            int.TryParse(Tbx_Age.Text, out int age);
            currSelPer.Name = name;
            currSelPer.Age = age;
            LvPeople.Items.Refresh();
            ResetFields();


        }

        private bool ArePersonInputsValid()
        {
            string name = Tbx_Name.Text;
            if (!Person.IsNameValid(name, out string errorName))
            {
                MessageBox.Show(this, errorName, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string strAge = Tbx_Age.Text;
            if (!Person.IsAgeValid(strAge, out string errorAge))
            {
                MessageBox.Show(this, errorAge, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void ResetFields()
        {
            Tbx_Name.Text = "";
            Tbx_Age.Text = "";
        }



        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person currSelPer = LvPeople.SelectedItem as Person;
            if (currSelPer == null)
            {
                ResetFields();
            }
            else
            {
                Tbx_Name.Text = currSelPer.Name;
                Tbx_Age.Text = currSelPer.Age.ToString();
            }
        }
    }
}

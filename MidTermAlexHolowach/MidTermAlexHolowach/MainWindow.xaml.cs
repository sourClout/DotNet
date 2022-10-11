using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace MidTermAlexHolowach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DataFileName = @"..\..\trip.txt";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Globals.DbContext = new TravelDbContext();


                LvTrip.ItemsSource = Globals.DbContext.ClassTrip.ToList();

            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }




        private void BtnAddTrip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!AreTripInputsValid()) return;
                string destination = TbxDest.Text;
                string name = TbxName.Text;
                string passport = TbxPass.Text;
                DateTime departDate = DpDepart.SelectedDate.Value;
                DateTime returnDate = DpReturn.SelectedDate.Value;

                Globals.DbContext.ClassTrip.Add(new Trip() { Destination = destination, TravellerName = name, TravellerPassport = passport, DepartureDate = departDate, ReturnDate = returnDate });
                Globals.DbContext.SaveChanges();
                LvTrip.ItemsSource = Globals.DbContext.ClassTrip.ToList();
                ResetFields();
                //LvTrip.SelectedItem = null;


            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdateTrip_Click(object sender, RoutedEventArgs e)
        {
            Trip currSelectedTrip = LvTrip.SelectedItem as Trip;
            if (currSelectedTrip == null) return;
            try
            {
                if (!AreTripInputsValid()) return;
                string destination = TbxDest.Text;
                string name = TbxName.Text;
                string passport = TbxPass.Text;
                DateTime departDate = DpDepart.SelectedDate.Value;
                DateTime returnDate = DpReturn.SelectedDate.Value;

                currSelectedTrip.Destination = destination;
                currSelectedTrip.TravellerName = name;
                currSelectedTrip.TravellerPassport = passport;
                currSelectedTrip.DepartureDate = departDate;
                currSelectedTrip.ReturnDate = returnDate;
                Globals.DbContext.SaveChanges();
                LvTrip.ItemsSource = Globals.DbContext.ClassTrip.ToList();
                ResetFields();
                LvTrip.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDeleteTrip_Click(object sender, RoutedEventArgs e)
        {
           
            Trip currSelectedTrip = LvTrip.SelectedItem as Trip;
            if (currSelectedTrip == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.DbContext.ClassTrip.Remove(currSelectedTrip);
                Globals.DbContext.SaveChanges();
                LvTrip.ItemsSource = Globals.DbContext.ClassTrip.ToList();
                ResetFields();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LvTrip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trip currSelectedTrip = LvTrip.SelectedItem as Trip;
            BtnUpdateTrip.IsEnabled = (currSelectedTrip != null);
            BtnDeleteTrip.IsEnabled = (currSelectedTrip != null);
            if (currSelectedTrip == null)
            {
                ResetFields();
            }
            else
            {
                TbxDest.Text = currSelectedTrip.Destination;
                TbxName.Text = currSelectedTrip.TravellerName;
                TbxPass.Text = currSelectedTrip.TravellerPassport;
                DpDepart.SelectedDate = currSelectedTrip.DepartureDate;
                DpReturn.SelectedDate = currSelectedTrip.ReturnDate;
            }
        }

        private void ResetFields()
        {
            TbxDest.Text = "";
            TbxName.Text = "";
            TbxPass.Text = "";
            DpDepart.SelectedDate = DateTime.Now;
            DpReturn.SelectedDate = DateTime.Now;

        }

        private void BtnSaveTrip_Click(object sender, RoutedEventArgs e)
        {
            SaveDataToFile();
        }

        private bool AreTripInputsValid()
        {
            string destination = TbxDest.Text;
            if (!Trip.IsDestinationValid(destination, out string errorDest))
            {
                MessageBox.Show(this, errorDest, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            string name = TbxName.Text;
            if (!Trip.IsTravellerNameValid(name, out string errorName))
            {
                MessageBox.Show(this, errorName, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

          
            string passport = TbxPass.Text;
            if (!Regex.IsMatch(passport, @"[a-zA-Z]{2}[0-9]{6}"))
            {
                MessageBox.Show("Invlaid passport format ex. AA123456", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }



            DateTime departDate = DpDepart.SelectedDate.Value;
            DateTime returnDate = DpReturn.SelectedDate.Value;
            if (!Trip.IsReturnDateValid(returnDate, departDate, out string errorDate))
            {
                MessageBox.Show(this, errorDate, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void SaveDataToFile()
        {
            List<string> lines = new List<string>();
            foreach (Trip t in LvTrip.SelectedItems)
            {
                lines.Add($"{t.TravellerName};{t.TravellerPassport};{t.Destination};{t.DepartureDate}; {t.ReturnDate} ");
            }
            try
            {
                File.WriteAllLines(DataFileName, lines);
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                MessageBox.Show(this, "Error reading from file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MessageBox.Show(this, "Data saved to file", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}

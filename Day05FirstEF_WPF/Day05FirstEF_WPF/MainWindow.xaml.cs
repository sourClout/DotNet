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

namespace Day05FirstEF_WPF
    // Steps to begin:
    // 1. Install Nugget Package
    // 2. Create Person.cs Class file --> Make sure Person is PUBLIC
    // 3. Create SocietyDbContext ADO.net DBO Model --> Empty Code first model
    // 4. Design XAML window + create CRUD event handlers
    // 5. Code Events methods (below)

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Need to add DB reference here (2 approaches this is the first below)
        // Second approach is create a class called Globals.cs and include: public static SocietyDbContext DbContext; (MUST BE PUBLIC ACCESS)
        // Need to open connection --> Done in XAML with handeler: Loaded="Window_Loaded"
        //SocietyDbContext dbContext;
        public MainWindow()
        {
            InitializeComponent();
        }

        // In this method will initialize connection to DB
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // To get true DbContext exceptions need to go to documentation in DbContext class
                Globals.DbContext = new SocietyDbContext(); // FIXME: exceptions
                // Globals DB Content here is used to GET the list of all people (put in memory for the ListView to later display/use)
                // Fetching from DB Table People and assinging to ItemSource
                // DO NOT need a field in Main window to remember what the items are because alwasy have access to them with ListView.Items
                LvPeople.ItemsSource = Globals.DbContext.People.ToList(); // equivalent of SELECT * FROM People (in DB table people)
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Most brutal way of ending application --> "YOU STOP NOW" --> temrinating application brutally
                // Only do this in most fatal catastrophic errors --> Here the APP only job is to manage info in the DB. If cant connect to DB no point of continuing
                Environment.Exit(1);
            }
        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = TbxName.Text; // FIXME! validation
                int.TryParse(TbxAge.Text, out int age); // FIXME! validation
                Globals.DbContext.People.Add(new Person() { Name = name, Age = age });
                Globals.DbContext.SaveChanges();
                // Re-Read data from database --> Fetch originally, modify (here its an ADD) then just re-fetch the whole thing. For classroom example, not large project.
                LvPeople.ItemsSource = Globals.DbContext.People.ToList(); // ex: equivalent of SELECT * FROM People
                ResetFields();
                LvPeople.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                // This exception is NOT terminal, just means an INSERT failed
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            Person currSelectedPerson = LvPeople.SelectedItem as Person;
            if (currSelectedPerson == null) return; // nothing selected, nothing to delete
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.DbContext.People.Remove(currSelectedPerson);
                Globals.DbContext.SaveChanges();
                // assigning a new list to ItemSource, old list gets Garbage collected
                LvPeople.ItemsSource = Globals.DbContext.People.ToList();
                ResetFields();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            // When you ask for this Person --> its still attached to Entity Framework (meaning this Lv Item is being fetched from the DB)
            Person currSelectedPerson = LvPeople.SelectedItem as Person;
            if (currSelectedPerson == null) return; // nothing selected so nothing to delete
            try
            {
                string name = TbxName.Text; // FIXME! validation
                int.TryParse(TbxAge.Text, out int age); // FIXME! validation
                // Entity Framework knows you are modifying this instance of a person
                currSelectedPerson.Name = name;
                currSelectedPerson.Age = age;
                // When you save changes --> those changes are pushed to the DB
                Globals.DbContext.SaveChanges();
               
                // Lv ItemSource is actually all items coming from DB (tracked by Entity Framework)
                LvPeople.ItemsSource = Globals.DbContext.People.ToList(); // ex: equivalent of SELECT * FROM People
                ResetFields();
                LvPeople.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person currSelectedPerson = LvPeople.SelectedItem as Person;
            BtnUpdatePerson.IsEnabled = (currSelectedPerson != null);
            BtnDeletePerson.IsEnabled = (currSelectedPerson != null);
            if (currSelectedPerson == null)
            {
                ResetFields();
            }
            else
            {
                TbxName.Text = currSelectedPerson.Name;
                TbxAge.Text = currSelectedPerson.Age.ToString();
            }
        }


        private void ResetFields()
        {
            TbxName.Text = "";
            TbxAge.Text = "";
        }

    }
}

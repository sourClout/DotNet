using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace day04ListGrid2ndAttempt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // datafile path 2 levels up from root to save data to
        const string DataFileName = @"..\..\people.txt";

        // Private since access level not declared
        // ListView has a property items or itemsource, need to change it to be property peopleList
        // The data of listView is a collection of object person (each person has properties name and age)
        // In XAML on Listview selection mode allows you to select an object from listview and eventually display it in textbox (for update and delete)
        // in XAML need an event for selectionChange since when value changes will load into textbox (works with delection mode above)
        List<Person> peopleList = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();
            // dont have the values null null when initially called, so it works. (faking having those values)
            // forcing an event at begining so action gets called right when the window is about to show --> not elegant solution but works
            // Finally, we set inital value in XAML is isEnabled = "false" on buttons update and delete
            // OR GREG Barre solution on buttons IsEnabled="{Binding SelectItems.Count, ElementName=LvPeople}"
            //LvPeople_SelectionChanged(null, null);
            // ListView has a property items or itemsource, need to change it to be property peopleList
            // when we add an item to peoplist ListView needs to be notified, THIS does NOT happen automitcally, needs to be notified
            LvPeople.ItemsSource = peopleList;
        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            // Validation done based on method below linking to Person.cs
            if (!ArePersonInputsValid()) return;
            // When doing an add we instantiate a person 
            string name = Tbx_Name.Text;
            int.TryParse(Tbx_Age.Text, out int age); // Okay: since we did validation at beginning --> no need to validate again
            // add Person to the peopeList then tell listView that data has been changed (next step below)
            peopleList.Add(new Person(name, age));
            //NOTIFY ListView that data has changed
            LvPeople.Items.Refresh();
            ResetFields();
        }

        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            // first check if something is selected
            Person currSelPer = LvPeople.SelectedItem as Person;
            if (currSelPer == null) return;
            // Ask user for confirmation on item's deletion
            // if answer is NO then we return
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;
            // if answer is not no (yes) then we remove the item --> refresh the list --> reset fields
            peopleList.Remove(currSelPer);
            LvPeople.Items.Refresh();
            ResetFields();


        }

        // need to know which person is selected (and if there is one) 
        // first check if there is a selection currently
        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            // ask listView what is the currently selected person --> need a selected item in order to update it
            Person currSelPer = LvPeople.SelectedItem as Person;
            if (currSelPer == null) return;
            if (!ArePersonInputsValid()) return;
            string name = Tbx_Name.Text;
            int.TryParse(Tbx_Age.Text, out int age);
            // HERE you're changing the name and age in the listView based on what update you write in textbox
            currSelPer.Name = name;
            currSelPer.Age = age;
            //NOTIFY ListView that data has changed
            LvPeople.Items.Refresh();
            ResetFields();
        }

        // When a selection changes need event handler to dispay it in textbox
        // The "Object" Sender in the below method is LvPeople --> the object from which the event came --> but in our solution we actually use the field LvPeople instead of sender
        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // read values of currently selected person and then put those values into the fields
            // ask listView what is the currently selected person
            // selected item gives us object type and we need to cast it as person (just something that needs to be done with selectedItem)
            // If selectedItem is not a class (or subclass) of type Person you will get a null
            Person currSelPer = LvPeople.SelectedItem as Person;
            // If a click happens (selection changed) and nothing happens we will clear the fields on the right hand side
            // example deselect a line by pressing CTRL

            // For Greg Barre IsEnabled solution need to comment below out to work
            BtnUpdatePerson.IsEnabled = (currSelPer != null);
            BtnDeletePerson.IsEnabled = (currSelPer != null);
            // Can also manipulate update/delete buttons this way --> so true/false based on if currSel null or not

            if (currSelPer == null)
            {
                ResetFields();
                // Enable / Disable buttons on SelectionChange --> If resetfields occurs it means nothing is selected so update/delete should be disabled
                //BtnUpdatePerson.IsEnabled = false;
                //BtnDeletePerson.IsEnabled = false;
            }
            else
            {
                //read values of currently selected person and put them into text boxes
                Tbx_Name.Text = currSelPer.Name;
                // can also do toString another way
                // Tbx_Age = cureSelPer.Age.ToString();
                Tbx_Age.Text = currSelPer.Age + "";

                // Enable update/delete buttons when you DO have a selection
                //BtnUpdatePerson.IsEnabled = true;
                //BtnDeletePerson.IsEnabled = true;


            }
        }

        // different way of doing validation
        private bool ArePersonInputsValid()
        {
            string name = Tbx_Name.Text;
            // Calling validator from Person class
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

        private void MiExport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MiExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MiSortName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MiSortAge_Click(object sender, RoutedEventArgs e)
        {

        }

        // add Loaded ="" event in XAML <window >
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // can also call this at top right after initializeComponent()
            LoadDataFromFile();
            LvPeople.Items.Refresh();
        }



        // add closing ="" event in XAML <window >
        // closing allows you to prevent the window from closing with message box are you sure you want to quit (window not gone, about to be closed)
        // in our case we use it to know when the closing is about to occur
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveDataToFile();
        }

        private void LoadDataFromFile()
        {
            try
            {
                // first thing is what if there is no file? its okay because first time you run the app there is no data, so nothing to read
                // (ignore if no file)
                // can be in or out of try {} makes no difference 
                if (!File.Exists(DataFileName)) return;
                // This collects a list of errors which may occur when reading --> display them all at once at the end in single messageBox
                List<string> errorsList = new List<string>();
                // need to read all lines from file then loop through them and split them into single objects with 2 items per line separated by ";"
                string[] linesArray = File.ReadAllLines(DataFileName); // ex IO/System
                // use for loop because foreach doesnt give the array/line number that error is on
                for (int i = 0; i < linesArray.Length; i++)
                {
                    string line = linesArray[i];
                    //var data = line.Split(';');
                    string[] data = line.Split(';');
                    if (data.Length != 2)
                    {
                        // the + line part shows the contents of the line --> thats a choice not always necessary
                        errorsList.Add($"Each line must have exactly 2 fields (line {i + 1})" + "\n" + line);
                        continue;
                    }
                    // once the string array is split and the errors are collected take the name and age at their index in the array and place them into variables
                    string name = data[0];
                    string ageStr = data[1];
                    // validate for name error using method from Person.cs
                    if (!Person.IsNameValid(name, out string errorName))
                    {
                        errorsList.Add($"{errorName} (line {i + 1}");
                        continue;
                    }
                    // validate and parse for int error using method from Person.cs
                    int.TryParse(ageStr, out int age);
                    if (!Person.IsAgeValid(ageStr, out string errorAge))
                    {
                        errorsList.Add($"{errorName} (line {i + 1}");
                        continue;
                    }
                    // Once everything checks out add the Objects to peopleList to be displayed in the listView UI
                    // since there are no setters that validate in Person we DO NOT get an exception here
                    // so dont need inner try-catch in this implementation since nothing to catch here
                    peopleList.Add(new Person(name, age));

                }
                // after for-loop need to check if ended up with errors or not
                if (errorsList.Count != 0)
                {
                    string allErrors = String.Join("\n", errorsList);
                    MessageBox.Show(this, $"Warning: some lines were ignored due to {errorsList.Count} errors:\n" + allErrors, "Data errors", MessageBoxButton.OK, MessageBoxImage.Error);
                }



            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                MessageBox.Show(this, "Error writing to file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveDataToFile()
        {
            // Taking the list of people in PeopleList and putting it into a List of strings to write into the saved txt file
            List<string> lines = new List<string>();
            foreach (Person p in peopleList)
            {
                //TODO: research CSV library to use here
                lines.Add($"{p.Name};{p.Age}");
            }
            try
            {
                File.WriteAllLines(DataFileName, lines);
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                MessageBox.Show(this, "Error reading from file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

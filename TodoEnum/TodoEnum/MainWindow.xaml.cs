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
using static TodoEnum.Todos;

namespace TodoEnum
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
        const string DataFileName = @"..\..\todos.txt";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Greg Barre code
            //ComboStatus.ItemsSource = Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>();
            try
            {
                // To get true DbContext exceptions need to go to documentation in DbContext class
                // Globals DB Content here is used to GET the list of all people (put in memory for the ListView to later display/use)
                // Fetching from DB Table People and assinging to ItemSource
                // DO NOT need a field in Main window to remember what the items are because alwasy have access to them with ListView.Items
                // FIXME: exceptions
                Globals.DbContext = new TdEnumDbContext();


                LvTodos.ItemsSource = Globals.DbContext.TodoEnum.ToList();
                // equivalent of SELECT * FROM People (in DB table people)

            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }


        }

        private void BtnAddTodo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!AreTodoInputsValid()) return;
                string task = TbxTask.Text; // 
                double difficulty = SliderDiff.Value; // 
                DateTime dueDate = DpDate.SelectedDate.Value;
                string status = ComboStatus.SelectedValue?.ToString();
                StatusEnum comboStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), status);

                /*
                StatusEnum status = (int)ComboStatus.SelectedItem;
                */

                Globals.DbContext.TodoEnum.Add(new Todos() { Task = task, Difficulty = difficulty, DueDate = dueDate, Status = comboStatus });
                Globals.DbContext.SaveChanges();
                // Re-Read data from database --> Fetch originally, modify (here its an ADD) then just re-fetch the whole thing. 
                LvTodos.ItemsSource = Globals.DbContext.TodoEnum.ToList();
                // ex: equivalent of SELECT * FROM People
                ResetFields();
                LvTodos.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                // This exception is NOT terminal, just means an INSERT failed
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void BtnDeleteTodo_Click(object sender, RoutedEventArgs e)
        {
            Todos currSelectedTodo = LvTodos.SelectedItem as Todos;
            if (currSelectedTodo == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.DbContext.TodoEnum.Remove(currSelectedTodo);
                Globals.DbContext.SaveChanges();
                // assigning a new list to ItemSource, old list gets Garbage collected
                LvTodos.ItemsSource = Globals.DbContext.TodoEnum.ToList();
                ResetFields();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnUpdateTodo_Click(object sender, RoutedEventArgs e)
        {
            Todos currSelectedTodo = LvTodos.SelectedItem as Todos;
            if (currSelectedTodo == null) return;
            try
            {
                if (!AreTodoInputsValid()) return;
                string task = TbxTask.Text; // 
                double difficulty = SliderDiff.Value; // 
                DateTime dueDate = DpDate.SelectedDate.Value;
                string status = ComboStatus.SelectedValue?.ToString();
                StatusEnum comboStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), status);
                // Entity Framework knows you are modifying this instance of a person
                currSelectedTodo.Task = task;
                currSelectedTodo.Difficulty = difficulty;
                currSelectedTodo.DueDate = dueDate;
                currSelectedTodo.Status = comboStatus;
                // When you save changes --> those changes are pushed to the DB
                Globals.DbContext.SaveChanges();

                // Lv ItemSource is actually all items coming from DB (tracked by Entity Framework)
                LvTodos.ItemsSource = Globals.DbContext.TodoEnum.ToList(); // ex: equivalent of SELECT * FROM People
                ResetFields();
                LvTodos.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnExportTodo_Click(object sender, RoutedEventArgs e)
        {
            SaveDataToFile();
        }

        private void LvTodos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Todos currSelectedTodo = LvTodos.SelectedItem as Todos;
            BtnUpdateTodo.IsEnabled = (currSelectedTodo != null);
            BtnDeleteTodo.IsEnabled = (currSelectedTodo != null);
            if (currSelectedTodo == null)
            {
                ResetFields();
            }
            else
            {
                TbxTask.Text = currSelectedTodo.Task;
                SliderDiff.Value = currSelectedTodo.Difficulty;
                DpDate.SelectedDate = currSelectedTodo.DueDate;
                ComboStatus.SelectedValue = currSelectedTodo.Status.ToString();
            }
        }

        private void ResetFields()
        {
            TbxTask.Text = "";
            SliderDiff.Value = 1;
            DpDate.SelectedDate = DateTime.Now;
            ComboPending.IsSelected = true;
        }


        private bool AreTodoInputsValid()
        {
            string task = TbxTask.Text;

            if (!Todos.IsTaskValid(task, out string errorTask))
            {
                MessageBox.Show(this, errorTask, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            DateTime dueDate = DpDate.SelectedDate.Value;
            if (!Todos.IsDateValid(dueDate, out string errorDate))
            {
                MessageBox.Show(this, errorDate, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        /*
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveDataToFile();
        }
        */

        private void SaveDataToFile()
        {
            // Taking the list of Todos in TodosList and putting it into a List of strings to write into the saved txt file
            List<string> lines = new List<string>();
            foreach (Todos t in LvTodos.ItemsSource)
            {
                //TODO: research CSV library to use here
                lines.Add($"{t.Task};{t.Difficulty};{t.DueDate.Date};{t.Status} ");
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Todos currSelectedTodo = LvTodos.SelectedItem as Todos;
            if (currSelectedTodo == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.DbContext.TodoEnum.Remove(currSelectedTodo);
                Globals.DbContext.SaveChanges();
                // assigning a new list to ItemSource, old list gets Garbage collected
                LvTodos.ItemsSource = Globals.DbContext.TodoEnum.ToList();
                ResetFields();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

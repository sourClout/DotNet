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
using System.Xml.Linq;

namespace TodosHomework
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             try
             {
                 // To get true DbContext exceptions need to go to documentation in DbContext class
                 Globals.DbContext = new TodoDbContext(); // FIXME: exceptions
                 // Globals DB Content here is used to GET the list of all people (put in memory for the ListView to later display/use)
                 // Fetching from DB Table People and assinging to ItemSource
                 // DO NOT need a field in Main window to remember what the items are because alwasy have access to them with ListView.Items
                 LvTodos.ItemsSource = Globals.DbContext.Todo.ToList(); // equivalent of SELECT * FROM People (in DB table people)
             }
             catch (SystemException ex)
             {
                 MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                 // Most brutal way of ending application --> "YOU STOP NOW" --> temrinating application brutally
                 // Only do this in most fatal catastrophic errors --> Here the APP only job is to manage info in the DB. If cant connect to DB no point of continuing
                 Environment.Exit(1);
             } 
        }

        private void BtnAddTodo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteTodo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdateTodo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LvTodos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void BtnExportTodo_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}

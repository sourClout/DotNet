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

namespace Day04ListView
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

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            // TODO: add validation
            string name = TbxName.Text;
            int.TryParse(TbxAge.Text, out int age);
            // Listview is a frontend component of WPF BUT has its own data layer --> a collection of items showing inside listview
            // Items is a collection 
            LvPeople.Items.Add($"{name} is {age} y/o");
            // clear the inputs
            TbxName.Text = "";
            TbxAge.Text = "";

        }
    }
}

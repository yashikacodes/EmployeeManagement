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

namespace Final_Yashika_Saini
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NorthWindEntities db;
        public MainWindow()
        {
            InitializeComponent();
            db = new NorthWindEntities();
        }

        private void btnGetAllEmployees_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = db.Employees.ToList();
        }

        private void btnSearchEmployeeByTitle_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a title to search.");
                return;
            }

            var result = from emp in db.Employees
                         where emp.Title.Contains(title)
                         select emp;

            dataGrid.ItemsSource = result.ToList();
        }

        private void btnInsertEmployee_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text) ||string.IsNullOrEmpty(txtTitle.Text) 
                )
            {
                MessageBox.Show("Please enter all required fields correctly.");
                return;
            }
            if (string.IsNullOrEmpty(txtBirthDate.Text)){
                MessageBox.Show("Enter birth date in format: mm/dd/yyyy hh:mm:ss");
                return;
            }

            Employee emp1 = new Employee();

            emp1.FirstName = txtFirstName.Text;
            emp1.LastName = txtLastName.Text;
            emp1.Title = txtTitle.Text;
            emp1.BirthDate = DateTime.Parse(txtBirthDate.Text);

            db.Employees.Add(emp1);
            db.SaveChanges();
            dataGrid.ItemsSource = db.Employees.ToList();
            MessageBox.Show("Employee Added");
            
        }

        private void btnGetOrders_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Please enter a valid ID.");
                return;
            }
            int id = int.Parse(txtID.Text);


            var orders = from order in db.Orders
                         where order.EmployeeID == id
                         select order;

            dataGrid.ItemsSource = orders.ToList();
 
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

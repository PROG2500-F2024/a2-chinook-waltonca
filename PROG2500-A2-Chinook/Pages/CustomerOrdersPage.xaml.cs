using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
using PROG2500_A2_Chinook.Models;
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

namespace PROG2500_A2_Chinook.Pages
{
    /// <summary>
    /// Interaction logic for CustomerOrdersPage.xaml
    /// </summary>
    public partial class CustomerOrdersPage : Page
    {
        private readonly ChinookContext _context = new ChinookContext();
        private CollectionViewSource customerOrdersViewSource = new CollectionViewSource();

        public CustomerOrdersPage()
        {
            InitializeComponent();
            customerOrdersViewSource = (CollectionViewSource)FindResource(nameof(customerOrdersViewSource));

            //Load data from the database
            _context.Customers.Load();
            _context.Invoices.Load();
            _context.InvoiceLines.Load();

            customerOrdersViewSource.Source = _context.Customers.Local.ToObservableCollection();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Define a grouping query to get grouped category data
            var query =
                from customer in _context.Customers
                where customer.LastName.Contains(textSearch.Text) //Search for the last name of the customer, I didn't search for the first name.
                orderby customer.LastName
                select new
                {
                    FullName = customer.FullName,
                    City = customer.City,
                    State = customer.State,
                    Country = customer.Country,
                    Email = customer.Email,
                    // Load invoices
                    Invoices = customer.Invoices.ToList<Invoice>()
                };

            //Execture the query against the db and assign it as the data source for the listview
            customerOrdersListVew.ItemsSource = query.ToList();
        }
    }
}

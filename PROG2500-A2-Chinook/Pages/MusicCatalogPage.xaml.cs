using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
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
    /// Interaction logic for MusicCatalogPage.xaml
    /// </summary>
    public partial class MusicCatalogPage : Page
    {
        private readonly ChinookContext _context = new ChinookContext();
        private CollectionViewSource musicCatalogViewSource = new CollectionViewSource();

        public MusicCatalogPage()
        {
            InitializeComponent();
            musicCatalogViewSource = (CollectionViewSource)FindResource(nameof(musicCatalogViewSource));

            //Load data from the database
            _context.Artists.Load();

            musicCatalogViewSource.Source = _context.Artists.Local.ToObservableCollection();


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Define a grouping query to get grouped category data
            var query =
                from artist in _context.Artists
                where artist.Name.Contains(textSearch.Text)
                select artist;

            //Execture the query against the db and assign it as the data source for the listview
            musicCatalogListVew.ItemsSource = query.ToList();
        }
    }
}

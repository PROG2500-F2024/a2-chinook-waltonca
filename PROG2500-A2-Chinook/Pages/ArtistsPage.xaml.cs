﻿using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ArtistsPage.xaml
    /// </summary>
    public partial class ArtistsPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource artistsViewSource = new CollectionViewSource();

        public ArtistsPage()
        {
            InitializeComponent();

            // Tie the markup xaml viewsource object to the code behind viewsource object(C#)
            artistsViewSource = (CollectionViewSource)FindResource(nameof(artistsViewSource));

            // Use the dbContext to tell EF to load the data we want to use on this page
            context.Artists.Load();

            // Set the viewsource data source to use the Products data collection (dbset)
            artistsViewSource.Source = context.Artists.Local.ToObservableCollection();
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = textSearch.Text.Trim();

            // USE LINQ
            // Define the query
            var query = from artist in context.Artists
                        where artist.Name.Contains(searchText)
                        orderby artist.ArtistId
                        select artist;


            // Execute the query
            artistsViewSource.Source = query.ToList();

        }
    }
}

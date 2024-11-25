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
    /// Interaction logic for AlbumsPage.xaml
    /// </summary>
    public partial class AlbumsPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource albumsViewSource = new CollectionViewSource();
        public AlbumsPage()
        {
            InitializeComponent();

            // Tie the markup xaml viewsource object to the code behind viewsource object(C#)
            albumsViewSource = (CollectionViewSource)FindResource(nameof(albumsViewSource));

            // Use the dbContext to tell EF to load the data we want to use on this page
            context.Albums.Load();

            // Set the viewsource data source to use the Products data collection (dbset)
            albumsViewSource.Source = context.Albums.Local.ToObservableCollection();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = textSearch.Text.Trim();

            // USE LINQ
            // Define the query
            var query = from album in context.Albums
                        where album.Title.Contains(searchText)
                        orderby album.ArtistId
                        select album;


            // Execute the query
            albumsViewSource.Source = query.ToList();
        }
    }
}

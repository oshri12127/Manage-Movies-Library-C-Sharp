using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace ManageMovies
{
    /// <summary>
    /// Interaction logic for AddDirector2MovieWindow.xaml
    /// </summary>
    public partial class AddDirector2MovieWindow : Window
    {
        public AddDirector2MovieWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                using (var ctx = new MoviesDBContext())
                {
                    lbDirectors.ItemsSource = (from d in ctx.Directors
                                              select d).ToList();
                    lbMovies.ItemsSource = (from c in ctx.Movies
                                              select c).ToList();
                }
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)//Add
        {

            if (lbDirectors.SelectedItem == null || lbMovies.SelectedItem == null)
            {
                MessageBox.Show("You must select a Director and a Movie");
                return;
            }
            try
            {
                Director selectedDirector = lbDirectors.SelectedItem as Director;
                Movie selectedMovie = lbMovies.SelectedItem as Movie;
                using (var ctx = new MoviesDBContext())
                {
                    Director director = (from d in ctx.Directors
                                         where d.Id == selectedDirector.Id
                                       select d).First();
                    Movie movie = (from m in ctx.Movies
                                    where m.MovieSerial == selectedMovie.MovieSerial
                                     select m).First();
                    movie.Director = director;
                    ctx.SaveChanges();
                    MessageBox.Show("Successfully added");
                }
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

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
    /// Interaction logic for AddMovie2ActressWindow.xaml
    /// </summary>
    public partial class AddMovie2ActressWindow : Window
    {
        public AddMovie2ActressWindow()
        {
            InitializeComponent();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                using (var ctx = new MoviesDBContext())
                {
                    lbActresses.ItemsSource = (from d in ctx.Actors
                                               where d.Gender == Gender.Female
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
                MessageBox.Show(ex.Message);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)//Add
        {

            if (lbActresses.SelectedItem == null || lbMovies.SelectedItem == null)
            {
                MessageBox.Show("You must select a Actress and a Movie");
                return;
            }
            try
            {
                Actor selectedActress = lbActresses.SelectedItem as Actor;
                Movie selectedMovie = lbMovies.SelectedItem as Movie;
                using (var ctx = new MoviesDBContext())
                {
                    Actor actor = (from a in ctx.Actors
                                   where a.Id == selectedActress.Id
                                   select a).First();
                    Movie movie = (from m in ctx.Movies
                                   where m.MovieSerial == selectedMovie.MovieSerial
                                   select m).First();
                    actor.ActorMovies.Add(new ActorMovie
                    {
                        IdNavigation = actor,
                        Id = actor.Id,
                        MovieSerialNavigation = movie,
                        MovieSerial = movie.MovieSerial
                    });

                    if (ctx.ActorMovie.Any(am=>am.Id==actor.Id && am.MovieSerial==movie.MovieSerial))
                        throw new Exception("Actress already in movie");
                    ctx.SaveChanges();
                    MessageBox.Show("Successfully added");
                }
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

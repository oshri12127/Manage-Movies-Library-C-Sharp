using System;
using System.Collections.Generic;
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
using System.Data.Common;

namespace ManageMovies
{
    /// <summary>
    /// Interaction logic for AddMovie2ActorWindow.xaml
    /// </summary>
    public partial class AddMovie2ActorWindow : Window
    {
        public AddMovie2ActorWindow()
        {
            InitializeComponent();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                using (var ctx = new MoviesDBContext())
                {
                    lbActors.ItemsSource = (from d in ctx.Actors
                                            where d.Gender == Gender.Male
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

            if (lbActors.SelectedItem == null || lbMovies.SelectedItem == null)
            {
                MessageBox.Show("You must select a Actor and a Movie");
                return;
            }
            try
            {
                Actor selectedActor = lbActors.SelectedItem as Actor;
                Movie selectedMovie = lbMovies.SelectedItem as Movie;
                using (var ctx = new MoviesDBContext())
                {
                    Actor actor = (from a in ctx.Actors
                                   where a.Id == selectedActor.Id
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
                    if (ctx.ActorMovie.Any(am => am.Id == actor.Id && am.MovieSerial == movie.MovieSerial))
                        throw new Exception("Actor already in movie");
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
                MessageBox.Show(ex.Message );
            }
        }
    }
}

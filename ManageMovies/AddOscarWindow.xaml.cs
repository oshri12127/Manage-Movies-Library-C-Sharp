using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManageMovies
{
    /// <summary>
    /// Interaction logic for AddOscarWindow.xaml
    /// </summary>
    public partial class AddOscarWindow : Window
    {
        public AddOscarWindow()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)//Add
        {
            try
            {
                if (!AllTextBoxesFilled())
                {
                    MessageBox.Show("You must fill all the data");
                    return;
                }
               
                using (var ctx = new MoviesDBContext())
                {
                    var splitActor = tbActor.Text.Split(' ');
                    var actorl = (from am in ctx.Actors
                                   where am.FirstName == splitActor.First() && am.LastName == string.Join(" ", splitActor.Skip(1)) && am.Gender==Gender.Male
                                   select am).ToList();
                    if (actorl.Count==0)
                    {
                        if (MessageBox.Show("No actor in Database. Do want to add him now?", "Notice", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            AddActorWindow window = new AddActorWindow();
                            window.SetNameActor(splitActor.First(), string.Join(" ", splitActor.Skip(1)));
                            window.ShowDialog(); 
                        }
                        actorl = (from am in ctx.Actors
                                 where am.FirstName == splitActor.First() && am.LastName == string.Join(" ", splitActor.Skip(1)) && am.Gender == Gender.Male
                                  select am).ToList();
                        if (actorl.Count == 0 )
                        {
                            throw new Exception("No actor in Database and refuses to add it");
                        }    
                    }
                    Actor actor = actorl.First();

                    var splitActress = tbActress.Text.Split(' ');
                    var actressL = (from af in ctx.Actors
                                   where af.FirstName == splitActress.First() && af.LastName == string.Join(" ", splitActress.Skip(1)) && af.Gender == Gender.Female
                                     select af).ToList();
                    if (actressL.Count == 0 )
                    {
                        if (MessageBox.Show("No actress in Database. Do want to add him now?", "Notice", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            AddActorWindow window = new AddActorWindow();
                            window.SetNameActoress(splitActress.First(), string.Join(" ", splitActress.Skip(1)));
                            window.ShowDialog();
                        }
                        actressL = (from af in ctx.Actors
                                 where af.FirstName == splitActress.First() && af.LastName == string.Join(" ", splitActress.Skip(1)) && af.Gender == Gender.Female
                                    select af).ToList();
                        if (actressL.Count == 0)
                        {
                            throw new Exception("No actress in Database and refuses to add it");
                        }
                    }
                    Actor actress = actressL.First();

                    var splitDir = tbDirector.Text.Split(' ');
                    var directorL = (from d in ctx.Directors
                                     where d.FirstName == splitDir.First() && d.LastName == string.Join(" ", splitDir.Skip(1))
                                     select d).ToList();
                    if (directorL.Count == 0)
                    {
                        if (MessageBox.Show("No director in Database. Do want to add him now?", "Notice", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            AddDirectorWindow window = new AddDirectorWindow();
                            window.SetNameDirector(splitDir.First(), string.Join(" ", splitDir.Skip(1)));
                            window.ShowDialog();
                        }
                        directorL = (from d in ctx.Directors
                                 where d.FirstName == splitDir.First() && d.LastName == string.Join(" ", splitDir.Skip(1))
                                     select d).ToList();
                        if (directorL.Count == 0)
                        {
                            throw new Exception("No director in Database and refuses to add it");
                        }
                    }
                    Director director = directorL.First();

                    var movieL = (from m in ctx.Movies
                                     where m.Title==tbMovie.Text.Trim()
                                     select m).ToList();
                    if (movieL.Count == 0)
                    {
                        if (MessageBox.Show("No movie in Database. Do want to add him now?", "Notice", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            AddMovieWindow window = new AddMovieWindow();
                            window.SetNameMovie(tbMovie.Text.Trim());
                            window.ShowDialog();
                        }
                        movieL = (from m in ctx.Movies
                                 where m.Title == tbMovie.Text.Trim()
                                 select m).ToList();
                        if (movieL.Count == 0)
                        {
                            throw new Exception("No movie in Database and refuses to add it");
                        }
                    }
                    Movie movie = movieL.First();

                    Oscar oscar2add = new Oscar
                    {
                        Year = int.Parse(tbYear.Text.Trim()),
                        Actor = actor,
                        Actress = actress,
                        Director=director,
                        MovieSerialNavigation = movie
                    };
                    ctx.Oscars.Add(oscar2add);
                    ctx.SaveChanges();
                }
                tbYear.Clear(); tbActor.Clear(); tbActress.Clear(); tbDirector.Clear(); tbMovie.Clear();
            }
             catch (ValidationException ve)
             {
                 MessageBox.Show(ve.Message + ": " + ve.BadString);
             }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
            catch (Exception ve)
            {
                MessageBox.Show(ve.Message);
            }
        }

        private bool AllTextBoxesFilled()
        {
            foreach (var item in AddOPPanel.Children)
            {
                if (item is TextBox && string.IsNullOrEmpty((item as TextBox).Text))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
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

namespace ManageMovies
{
    /// <summary>
    /// Interaction logic for AddMovieWindow.xaml
    /// </summary>
    public partial class AddMovieWindow : Window
    {
        public AddMovieWindow()
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

                Movie movie2add = new Movie
                {
                    Title = tbTitle.Text.Trim(),
                    Year = int.Parse(tbYear.Text.Trim()),
                    Country = tbCountry.Text.Trim(),
                    ImdbScore = decimal.Parse(tbIMBD.Text.Trim())
                };
                using (var ctx = new MoviesDBContext())
                {
                    ctx.Movies.Add(movie2add);
                    ctx.SaveChanges();
                }
                tbTitle.Clear(); tbYear.Clear(); tbIMBD.Clear(); tbCountry.Clear();
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
            foreach (var item in AddMPPanel.Children)
            {
                if (item is TextBox && string.IsNullOrEmpty((item as TextBox).Text))
                {
                    return false;
                }
            }
            return true;
        }

        internal void SetNameMovie(string title)//set name movie from Oscar window
        {
            tbTitle.Text = title;
            tbTitle.IsReadOnly = true;
        }
    }
}

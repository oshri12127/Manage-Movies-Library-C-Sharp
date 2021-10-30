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
    /// Interaction logic for AddDirectorWindow.xaml
    /// </summary>
    public partial class AddDirectorWindow : Window
    {
        public AddDirectorWindow()
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

                Director director2add = new Director
                {
                    Id = int.Parse(tbId.Text.Trim()),
                    FirstName = tbFirstName.Text.Trim(),
                    LastName = tbLastName.Text.Trim(),
                    
                };
                using (var ctx = new MoviesDBContext())
                {
                    ctx.Directors.Add(director2add);
                    ctx.SaveChanges();
                }
                tbId.Clear(); tbFirstName.Clear(); tbLastName.Clear();
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

        internal void SetNameDirector(string firstName, string LastName)//set name Director from oscar window
        {
            tbFirstName.Text = firstName;
            tbLastName.Text = LastName;
            tbFirstName.IsReadOnly = true;
            tbLastName.IsReadOnly = true;
        }
    }
}


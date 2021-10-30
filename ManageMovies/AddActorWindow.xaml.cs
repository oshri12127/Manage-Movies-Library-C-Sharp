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
    /// Interaction logic for AddActorWindow.xaml
    /// </summary>
    public partial class AddActorWindow : Window
    {
        public AddActorWindow()
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

                Actor actor2add = new Actor
                {
                    Id=int.Parse(tbId.Text.Trim()),
                    FirstName = tbFirstName.Text.Trim(),
                    LastName = tbLastName.Text.Trim(),
                    Gender = checkGender(),
                    YearBorn = int.Parse(tbYearDate.Text.Trim())
                };
                using (var ctx = new MoviesDBContext())
                {
                    ctx.Actors.Add(actor2add);
                    ctx.SaveChanges();
                }
                tbId.Clear(); tbFirstName.Clear(); tbLastName.Clear(); tbYearDate.Clear();
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

        private Gender checkGender()
        {
            if (cbGender.Text == "Female")
            {
                return Gender.Female;
            }
            return Gender.Male;
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

        internal void SetNameActoress(string firstName, string LastName)//set name Actoress from Oscar window
        {
            tbFirstName.Text = firstName;
            tbLastName.Text = LastName;
            tbFirstName.IsReadOnly = true;
            tbLastName.IsReadOnly = true;
            cbGender.SelectedIndex = 1;
            cbGender.IsEnabled = false;
        }
        internal void SetNameActor(string firstName, string LastName)//set name Actor from Oscar window
        {
            tbFirstName.Text =firstName;
            tbLastName.Text = LastName;
            tbFirstName.IsReadOnly = true;
            tbLastName.IsReadOnly = true;
            cbGender.SelectedIndex = 0;
            cbGender.IsEnabled = false;
        }
    }
}

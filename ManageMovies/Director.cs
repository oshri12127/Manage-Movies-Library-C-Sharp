using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ManageMovies
{
    public partial class Director
    {
        public Director()
        {
            Movies = new HashSet<Movie>();
            Oscars = new HashSet<Oscar>();
        }
        static string namePattern = @"^[A-Za-z]*(\s+[A-Za-z]*){0,3}$";
        static Regex rgName = new Regex(namePattern);
        public int Id { get; set; }
        private string firstName;
        public string FirstName
        {
            set
            {
                if (!rgName.IsMatch(value) || value == "")
                    throw new ValidationException("Invalid First name,only Characters. Input", value);
                firstName = value;
            }
            get { return firstName; }
        }
        private string lastName;
        public string LastName
        {
            set
            {
                if (!rgName.IsMatch(value)||value=="")
                    throw new ValidationException("Invalid Last name,only Characters. Input", value);
                lastName = value;
            }
            get { return lastName; }
        }

        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Oscar> Oscars { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName},ID:{Id}";
        }
    }
}

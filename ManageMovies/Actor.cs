using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ManageMovies
{
    public enum Gender
    {
        Male,
        Female
    };
    public partial class Actor
    {
        public Actor()
        {
            ActorMovies = new HashSet<ActorMovie>();
            OscarsActor = new HashSet<Oscar>();
            OscarsActress = new HashSet<Oscar>();
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
                if (!rgName.IsMatch(value) || value == "")
                    throw new ValidationException("Invalid Last name,only Characters. Input", value);
                lastName = value;
            }
            get { return lastName; }
        }
        private int yearBorn;
        public int YearBorn
        {
            get
            {
                return yearBorn;
            }
            set
            {
                string yearPattern = @"^(19[0-9][0-9]|20[01][0-9]|2010)$";
                if (!Regex.Match(value.ToString(), yearPattern).Success)
                {
                    throw new ValidationException("Invalid Year born,must be between 1900 to 2010. Input", value.ToString());
                }
                yearBorn = value;
            }
        }
        public Gender Gender { get; set; }

        public virtual ICollection<ActorMovie> ActorMovies { get; set; }
        public virtual ICollection<Oscar> OscarsActor { get; set; }
        public virtual ICollection<Oscar> OscarsActress { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName},ID:{Id},Year:{YearBorn}," +
                $"Gender:{Gender}";
        }
    }
}

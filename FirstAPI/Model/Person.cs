using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAPI.Model
{
    public class Person
    {
        public long Id{ get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string Email  { get; set; }
        public string Gender  { get; set; }


        public Person ()
        {

        }

        public Person(int id, string firtsName, string lastName, string email, string gender)
        {
            Id = id;
            FirstName = firtsName;
            LastName = lastName;
            Email = email;
            Gender = gender;
        }
    }
}

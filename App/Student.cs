using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class Student
    {
        //EF core uses refection to assign values to the backing fields for properties
        // hence you can use private setters
        // you can also entirely remove setters
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public long FavoriteCourseId { get; }

        public Student(string name, 
            string email, long favoriteCourseId)
        {
            Name = name;
            Email = email;
            FavoriteCourseId = favoriteCourseId;
        }
    }
}

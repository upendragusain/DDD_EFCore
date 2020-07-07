using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class Student : Entity
    {
        //EF core uses refection to assign values to the backing fields for properties
        // hence you can use private setters
        // you can also entirely remove setters
        // only for primitive types!
        public string Name { get; private set; }
        public string Email { get; private set; }
        public virtual Course FavoriteCourse { get; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        //to satisfy the orm (EF), EF can apparently still access it (after change to protected), perhaps with reflection?
        //needed as the other ctor has a no primitive type as a parameter
        //System.InvalidOperationException: 'No suitable constructor found for entity type 'Student'. The following constructors had parameters that could not be bound to properties of the entity type: cannot bind 'favoriteCourse' in 'Student(string name, string email, Course favoriteCourse)'.'
        protected Student()
        {

        }

        public Student(string name, 
            string email, Course favoriteCourse)
        {
            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;
        }
    }
}

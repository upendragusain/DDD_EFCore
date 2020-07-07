using System;

namespace App
{
    public abstract class Entity
    {
        public long Id { get; }

        protected Entity()
        {
        }

        protected Entity(long id)
            : this()
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other))
                return false;

            // this will be true even for entities that are a result of different db calls
            // perhaps due to EF indetity map cache, it stored it as the same entity after retrival
            // ** Referential Equality, if you request the same object 2 times the dbcontext will give you 2 references to the same object in memory
            if (ReferenceEquals(this, other))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public static bool operator == (Entity a, Entity b)
        {
            // checks for null because it' a static method and not an instance one
            // hence can be invoked on null
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }

        private Type GetRealType()
        {
            Type type = GetType();

            if (type.ToString().Contains("Castle.Proxies."))
                return type.BaseType;

            return type;
        }
    }
}

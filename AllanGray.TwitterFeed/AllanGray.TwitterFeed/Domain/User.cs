using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Domain
{
    public class User
    {
        private string _name;

        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name");

            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return _name == other._name;
        }

        public static implicit operator User(string user)
        {
            return new User(user);
        }
    }
}

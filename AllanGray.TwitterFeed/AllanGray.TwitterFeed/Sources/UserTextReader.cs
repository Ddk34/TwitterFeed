using AllanGray.TwitterFeed.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Sources
{
    public class UserTextReader : TextReader<UserDetail>, IUserReader
    {
        private static readonly string[] _userAndFollowsSplit = new string[] { " follows " };
        private static readonly string[] _followsSplit = new string[] { ", " };

        public UserTextReader(string path) :
            base(path)
        { }

        protected override UserDetail Parse(string line)
        {
            var lineItems = line.Split(_userAndFollowsSplit, StringSplitOptions.RemoveEmptyEntries);

            if (lineItems.Length != 2)
                throw InvalidFormatException.Create(line);

            try
            {
                return new UserDetail()
                {
                    User = lineItems[0],
                    Follows = ParseFollows(lineItems[1])
                };
            }
            catch(Exception e)
            {
                throw InvalidFormatException.Create(line, e);
            }
        }

        private IEnumerable<User> ParseFollows(string follows)
        {
            if (string.IsNullOrWhiteSpace(follows))
                throw new InvalidFormatException("No followers specified");

            var users = follows.
                        Split(_followsSplit, StringSplitOptions.RemoveEmptyEntries)
                        .Select(u => (User)u)
                        .ToArray();

            if (users == null || users.Length == 0)
                throw new InvalidFormatException("No followers specified");

            return users;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Domain
{
    public class UserDetail
    {
        public User User { get; set; }
        public IEnumerable<User> Follows { get; set; }
    }
}

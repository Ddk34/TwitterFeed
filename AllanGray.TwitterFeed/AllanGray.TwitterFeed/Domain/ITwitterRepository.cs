using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Domain
{
    public interface ITwitterRepository
    {
        void SaveUser(User user, IEnumerable<User> follows = null);
        void SaveTweet(Tweet tweet);
        IEnumerable<UserFeed> FetchUserFeeds();
    }
}

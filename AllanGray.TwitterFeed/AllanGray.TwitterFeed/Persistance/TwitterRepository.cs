using AllanGray.TwitterFeed.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed
{
    public class TwitterRepository : ITwitterRepository
    {
        private Dictionary<User, UserFeed> _userFeeds;

        public TwitterRepository()
        {
            _userFeeds = new Dictionary<User, UserFeed>();
        }

        public void SaveUser(User user, IEnumerable<User> follows = null)
        {
            CreateOrGetUserFeed(user);

            if (follows == null)
                return;

            foreach (var follow in follows)
            {
                UserFeed userFeed = CreateOrGetUserFeed(follow);
                userFeed.AddFollower(user);
            }

        }

        private UserFeed CreateOrGetUserFeed(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (!_userFeeds.ContainsKey(user))
                _userFeeds.Add(user, new UserFeed(user));

            return _userFeeds[user];
        }

        public void SaveTweet(Tweet tweet)
        {
            if (tweet == null)
                throw new ArgumentNullException("tweet");

            UserFeed userFeed = CreateOrGetUserFeed(tweet.Creator);
            userFeed.AddTweet(tweet);

            foreach (var follower in userFeed.FollowedBy)
            {
                UserFeed followerFeed = CreateOrGetUserFeed(follower);
                followerFeed.AddTweet(tweet);
            }
        }

        public IEnumerable<UserFeed> FetchUserFeeds()
        {
            return _userFeeds.Values;
        }
    }
}

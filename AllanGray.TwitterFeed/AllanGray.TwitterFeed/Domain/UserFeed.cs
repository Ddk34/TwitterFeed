using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Domain
{
    public class UserFeed
    {
        private List<Tweet> _tweets;
        private HashSet<User> _followedBy;

        public IEnumerable<User> FollowedBy
        {
            get
            {
                return _followedBy;
            }
        }

        public IEnumerable<Tweet> Tweets
        {
            get
            {
                return _tweets;
            }
        }

        public User User { get; private set; }

        public UserFeed(User name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            _tweets = new List<Tweet>();
            _followedBy = new HashSet<User>();

            User = name;
        }

        public void AddTweet(Tweet tweet)
        {
            if (tweet == null)
                throw new ArgumentNullException("tweet");

            _tweets.Add(tweet);
        }

        public void AddFollower(User follower)
        {
            if (follower == null)
                throw new ArgumentNullException("follower");

            _followedBy.Add(follower);
        }


    }
}

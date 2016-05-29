using AllanGray.TwitterFeed.Domain;
using AllanGray.TwitterFeed.Sources;
using AllanGray.TwitterFeed.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Domain
{
    public class TwitterFeedService
    {
        private readonly Dictionary<User, UserFeed> _userFeeds;
        
        private readonly IUserReader _userReader;
        private readonly ITweetReader _tweetReader;
        private readonly IView<IEnumerable<UserFeed>> _view;

        /// <summary>
        /// The writeLine Action allows to write to anything. Maybe another file. Please see unit test for example.
        /// </summary>
        public TwitterFeedService(string userSource, string tweetSource, Action<string> writeLine) :
            this(new UserTextReader(userSource), new TweetTextReader(tweetSource), new TwitterFeedView(writeLine))
        { }

        /// <summary>
        /// I use Poor Man's DI, to illustrate that the sources of Users/Followers and Tweet could come from other sources, like an XML file etc.
        /// </summary>
        internal TwitterFeedService(IUserReader userReader, ITweetReader tweetReader, IView<IEnumerable<UserFeed>> view)
        {
            _userFeeds = new Dictionary<User, UserFeed>();

            _userReader = userReader;
            _tweetReader = tweetReader;
            _view = view;
        }

        /// <summary>
        /// Assumption: If any data is invalid the whole feed stops processing.
        /// </summary>
        public void ProcessAndDisplay()
        {
            foreach (var user in _userReader.ReadAll())
            {
                AddUser(user.User, user.Follows);
            }

            foreach (var tweet in _tweetReader.ReadAll())
            {
                AddTweet(tweet);
            }

            _view.Display(_userFeeds.Values.OrderBy(uf => uf.User.ToString()));
        }

        private void AddUser(User user, IEnumerable<User> follows)
        {
            CreateOrGetUserFeed(user);

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

        private void AddTweet(Tweet tweet)
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
    }
}

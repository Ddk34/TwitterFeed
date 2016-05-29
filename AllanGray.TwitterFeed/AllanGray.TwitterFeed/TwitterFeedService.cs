using AllanGray.TwitterFeed.Domain;
using AllanGray.TwitterFeed.Sources;
using AllanGray.TwitterFeed.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed
{
    public class TwitterFeedService
    {
        private readonly ITwitterRepository _repo;
        private readonly IUserReader _userReader;
        private readonly ITweetReader _tweetReader;
        private readonly IView<IEnumerable<UserFeed>> _view;

        public TwitterFeedService(string userSource, string tweetSource, Action<string> writeLine) :
            this(new UserTextReader(userSource), new TweetTextReader(tweetSource), new TwitterRepository(), new TwitterFeedView(writeLine))
        { }

        internal TwitterFeedService(IUserReader userReader, ITweetReader tweetReader, ITwitterRepository repo, IView<IEnumerable<UserFeed>> view)
        {
            _repo = repo;
            _userReader = userReader;
            _tweetReader = tweetReader;
            _view = view;
        }

        public void Display()
        {
            foreach (var user in _userReader.ReadAll())
            {
                _repo.SaveUser(user.User, user.Follows);
            }

            foreach (var tweet in _tweetReader.ReadAll())
            {
                _repo.SaveTweet(tweet);
            }

            _view.Display(_repo.FetchUserFeeds().OrderBy(uf => uf.User.ToString()));
        }

    }
}

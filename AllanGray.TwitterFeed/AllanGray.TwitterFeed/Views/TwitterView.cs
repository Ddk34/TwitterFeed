using AllanGray.TwitterFeed.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Views
{
   

    public class TwitterFeedView : IView<IEnumerable<UserFeed>>
    {
        private Action<string> _writer;

        public TwitterFeedView(Action<string> writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            _writer = writer;
        }

        public void Display(IEnumerable<UserFeed> feeds)
        {
            if (feeds == null)
                throw new ArgumentNullException("feeds");

            foreach(var feed in feeds)
            {
                _writer(feed.User.ToString());

                foreach(var tweet in feed.Tweets)
                {
                    _writer($"\t{tweet}");
                }
            }
        }
    }
}

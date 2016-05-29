using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Domain
{
    public class Tweet
    {
        private const int _maxTweetLength = 140;

        public User Creator { get; private set; }
        public string Text { get; private set; }

        public Tweet(User creator, string text)
        {
            if (creator == null)
                throw new ArgumentNullException("creator");

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException("text");

            if (text.Length > 140)
                throw new ArgumentException($"Tweet can not exceed {_maxTweetLength} characters.");

            Creator = creator;
            Text = text;
        }

        public override string ToString()
        {
            return $"@{Creator}: {Text}";
        }
    }
}

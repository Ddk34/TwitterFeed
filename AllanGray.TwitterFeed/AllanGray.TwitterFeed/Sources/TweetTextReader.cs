using AllanGray.TwitterFeed.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Sources
{
   
    public class TweetTextReader : TextReader<Tweet> , ITweetReader
    {
        public TweetTextReader(string path) :
            base(path)
        { }

        protected override Tweet Parse(string line)
        {
            int index = line.IndexOf("> ");

            if (index < 0)
                InvalidFormatException.Create(line);

            User creator = line.Substring(0, index);

            index += 2;

            if (index > line.Length)
                InvalidFormatException.Create(line);

            string text = line.Substring(index, line.Length - index);

            return new Tweet(creator, text);
        }
    }
}

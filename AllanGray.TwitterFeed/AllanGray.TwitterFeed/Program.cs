using AllanGray.TwitterFeed.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed
{
    class Program
    {
        static int Main(string[] args)
        {
            int result = 0;

            try
            {
                string userSource, tweetSource;
                ParseArgs(args, out userSource, out tweetSource);

                var twitterService = new TwitterFeedService(userSource, tweetSource, Console.WriteLine);
                twitterService.ProcessAndDisplay();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = -1;
            }

            Console.ReadLine();
            return result;
        }

        static void ParseArgs(string[] args, out string usersSource, out string tweetSource)
        {
            usersSource = tweetSource = null;

            if (args == null)
                throw new Exception();

            if (args.Length != 2)
                throw new Exception();

            if (string.IsNullOrWhiteSpace(args[0]))
                throw new Exception();

            usersSource = args[0];

            if (string.IsNullOrWhiteSpace(args[1]))
                throw new Exception();

            tweetSource = args[1];

        }
    }
}

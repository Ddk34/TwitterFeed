using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;
using AllanGray.TwitterFeed.Domain;

namespace AllanGray.TwitterFeed.Tests
{
    [TestClass]
    public class DisplayFilesTest
    {
        [TestMethod]
        public void Display()
        {
            StringBuilder sb = new StringBuilder(); 

            var twitterService = new TwitterFeedService("users.txt", "tweets.txt",(line)=> sb.AppendLine(line));
            twitterService.ProcessAndDisplay();

            var splitBy = new string[] { "\r\n" };

            var expected = File.ReadAllText("DisplayText.txt").Split(splitBy, StringSplitOptions.None);
            var actual = sb.ToString().Split(splitBy, StringSplitOptions.None);

            for(int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }
    }
}

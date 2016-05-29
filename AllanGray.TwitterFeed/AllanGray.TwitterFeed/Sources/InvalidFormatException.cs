using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Sources
{
    public class InvalidFormatException : Exception
    {
        public InvalidFormatException() :
            base()
        { }

        public InvalidFormatException(string message) :
           base(message)
        { }

        public InvalidFormatException(string message, Exception inner) :
           base(message, inner)
        { }

        public static InvalidFormatException Create(int line, Exception inner)
        {
            return new InvalidFormatException($"line: {line}.", inner);
        }

        public static InvalidFormatException EmptyOrNull()
        {
            return new InvalidFormatException("Empty or Null line.");
        }

        public static InvalidFormatException Create(string line)
        {
            return new InvalidFormatException($"Couldnt parse data: {line}.");
        }

        public static InvalidFormatException Create(string line, Exception inner)
        {
            return new InvalidFormatException($"Couldnt parse data: {line}.", inner);
        }
    }
}

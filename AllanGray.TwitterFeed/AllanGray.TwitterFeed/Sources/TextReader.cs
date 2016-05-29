using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Sources
{
    public abstract class TextReader<T>
        where T : class
    {
        private string _path;

        public TextReader(string path)
        {
            _path = path;
        }

        public IEnumerable<T> ReadAll()
        {
            using (var reader = File.OpenText(_path))
            {
                int lineNo = 0;
                T parsedLine = null;
                while (reader.Peek() >= 0)
                {
                    try
                    {
                        lineNo++;

                        string line = reader.ReadLine();

                        if (string.IsNullOrWhiteSpace(line))
                            InvalidFormatException.EmptyOrNull();

                        parsedLine = Parse(line);
                    }
                    catch (Exception e)
                    {
                        throw InvalidFormatException.Create(lineNo, e);
                    }

                    yield return parsedLine;
                }
            }
        }

        protected abstract T Parse(string line);
    }
}

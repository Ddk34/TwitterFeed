using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Domain
{
    public interface ITweetReader
    {
        IEnumerable<Tweet> ReadAll();
    }
}

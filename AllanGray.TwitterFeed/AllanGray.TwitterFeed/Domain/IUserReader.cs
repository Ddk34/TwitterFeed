using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllanGray.TwitterFeed.Domain
{
    public interface IUserReader
    {
        IEnumerable<UserDetail> ReadAll();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataTransaction : IDisposable
    {
        void Commit ();
        void Rollback();
    }
}

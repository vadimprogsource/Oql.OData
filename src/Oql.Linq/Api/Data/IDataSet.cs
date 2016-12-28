using Oql.Linq.Api.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{


    public interface IDataStruct
    {
        object[] GetRecord(Type[] types);
        
        T GetValueByIndex<T>(int index);

        T GetValueByName<T>(string name);
    }

    public interface IDataSet : IEnumerable<IDataStruct>, IDisposable
    {
    }
}

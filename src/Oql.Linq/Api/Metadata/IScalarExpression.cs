using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{


    public enum ScalarFuction : byte {Get, Avg , Sum ,Min, Max , Count };


    public interface IScalarExpression
    {
        ScalarFuction Function { get; }
        string From { get; }
        string Expression { get; }
        string Where { get; }
        string OrderBy { get; }
        string Body { get; }
    }
}

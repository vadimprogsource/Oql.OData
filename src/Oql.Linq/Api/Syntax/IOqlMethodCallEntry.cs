
using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Syntax.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax
{
    public interface IOqlMethodCallEntry
    {
        IEnumerable<IMethod> GetMethods();
        IOqlClause              Clause    { get; }
        IOqlMethodCallFormatter Formatter { get; }
    }
}

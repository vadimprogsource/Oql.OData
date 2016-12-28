using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IExpression
    {

        IExpression  Expression {get;}

        string Name  { get; }
        object Value { get; }

        bool Asc { get; }
        bool Desc { get; }

        bool Not { get; }
        bool And { get; }
        bool Or { get; }
        bool Xor { get; }

        bool Eq { get; }
        bool Ne { get; }

        bool Gt { get; }
        bool Ge { get; }

        bool Lt { get; }
        bool Le { get; }

        bool IsOf { get; }

        bool Add { get; }
        bool Sub { get; }
        bool Mul { get; }
        bool Div { get; }
        bool Mod { get; }

        bool Negate { get; }

        bool IsMethodCall { get; }

    }
}

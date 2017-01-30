using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IScalarExpressionBuilder
    {
        IScalarExpressionBuilder From(string source);
        IScalarExpressionBuilder Avg(string operand);
        IScalarExpressionBuilder Sum(string operand);
        IScalarExpressionBuilder Min(string operand);
        IScalarExpressionBuilder Max(string operand);
        IScalarExpressionBuilder Count();
        IScalarExpressionBuilder Where(string condition);
        IScalarExpressionBuilder OrderBy(string operand);
    }
}

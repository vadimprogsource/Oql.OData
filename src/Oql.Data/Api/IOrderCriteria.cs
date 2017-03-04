using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Data.Api
{
    public interface IOrderCriteria
    {
         Expression PropertyOrField { get; }
         bool Asc { get; }
    }
}

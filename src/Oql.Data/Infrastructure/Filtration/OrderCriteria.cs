using Oql.Data.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Oql.Data.Infrastructure.Filtration
{
    public class OrderCriteria : IOrderCriteria
    {
        public Expression PropertyOrField { get; set; }
        public bool Asc { get; set; }

    }
}

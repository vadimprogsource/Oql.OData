using Oql.Data.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Oql.Data.Infrastructure.Filtration
{
    public class FilterCriteria : IFilterCriteria
    {
        public Expression PropertyOrField { get; set; }
        public object Value { get; set; } 
      
    }
}

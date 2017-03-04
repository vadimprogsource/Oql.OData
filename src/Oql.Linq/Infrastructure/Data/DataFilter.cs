using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Data
{

    public class DataFilterCriteria : IDataFilterCriteria
    {
        public string PropertyOrField { get; set; }
        public object Value { get; set; }

    }


    public class DataOrderCriteria : IDataOrderCriteria
    {
        public string PropertyOrField { get; set; }
        public bool Asc { get; set; }

    }

    public class DataFilter : IDataFilter
    {
        private List<DataFilterCriteria> data_where    = new List<DataFilterCriteria>();
        private List<DataOrderCriteria>  data_order_by = new List<DataOrderCriteria> ();

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool HasFiltered
        {
            get
            {
                return data_where.Count > 0;
            }
        }

        public bool HasOrdered
        {
            get
            {
                return data_order_by.Count > 0;
            }
        }

        public IEnumerable<IDataFilterCriteria> Where
        {
            get
            {
                return data_where;
            }
        }

        public IEnumerable<IDataOrderCriteria> OrderBy
        {
            get
            {
                return data_order_by;
            }
        }

        public DataFilter AddWhere(DataFilterCriteria criteria)
        {
            data_where.Add(criteria);
            return this;
        }

        public DataFilter AddOrderBy(DataOrderCriteria criteria)
        {
            data_order_by.Add(criteria);
            return this;
        }
    }
}

using Oql.Data.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Data.Infrastructure.Filtration
{
    public class Filter : IFilter
    {
        private List<FilterCriteria> m_where    = new List<FilterCriteria>();
        private List<OrderCriteria>  m_order_by = new List<OrderCriteria> ();

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool HasFiltered
        {
            get
            {
                return m_where.Count > 0;
            }
        }

        public bool HasOrdered
        {
            get
            {
                return m_order_by.Count > 0;
            }
        }

        public IEnumerable<IFilterCriteria> Where
        {
            get
            {
                return m_where;
            }
        }

        public IEnumerable<IOrderCriteria> OrderBy
        {
            get
            {
                return m_order_by;
            }
        }

        public Filter AddWhere(FilterCriteria criteria)
        {
            m_where.Add(criteria);
            return this;
        }

        public Filter AddOrderBy(OrderCriteria criteria)
        {
            m_order_by.Add(criteria);
            return this;
        }
    }
}

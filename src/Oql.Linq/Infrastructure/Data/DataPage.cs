using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Data
{
    public class DataPage<T> : IDataPage<T>
    {
        private T[] data_page;
        private int data_total_recs;




        public DataPage(int pageSize , int pageIndex , IQueryable<T> query)
        {
            data_total_recs = query.Count();

            int skipped = pageIndex * PageSize;

            if (skipped > 0 && skipped < data_total_recs)
            {
                query = query.Skip(skipped);
            }

            if (data_total_recs > pageSize)
            {
                query = query.Take(pageSize);
            }

            data_page = query.ToArray();


        }



        public IEnumerable<T> Page
        {
            get
            {
                return data_page;
            }
        }

        public int PageIndex { get; private set; }
      
        public int Pages
        {
            get
            {
                int p = PageSize ,  r = data_total_recs / p;

                if (data_total_recs % p > 0)
                {
                    ++r;
                }

                return r;

            }
        }

        public int PageSize { get; private set; }

        public int TotalRecs 
        {
            get
            {
                return data_total_recs;
            }
        }
    }
}

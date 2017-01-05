using Oql.Linq.Api.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Oql.Linq.Api.Data;
using Oql.Linq.Api;

namespace Oql.Linq.Infrastructure.Query
{
    public abstract class ObjectQueryNavigator
    {
        public abstract object GetResult(IOqlCallResult callResult);


        public static object CreateResult(IOqlCallResult callResult,IDataSource dataSource,IQueryBuilder query)
        {
                return (Activator.CreateInstance(typeof(ObjectQueryIterator<>).MakeGenericType(callResult.ResultType), dataSource, query) as ObjectQueryNavigator)
                       .GetResult(callResult);
          }


    }

    public abstract class ObjectQueryNavigator<T> : ObjectQueryNavigator , IEnumerable<T>
    {
       private static Func<IEnumerable<T>, int, T>[,] m_navigate_getters = new Func<IEnumerable<T>, int, T>[,]
       {
            {(x,y)=>x.Single()      , (x,y)=>x.SingleOrDefault() },
            {(x,y)=>x.First()       , (x,y)=>x.FirstOrDefault()  },
            {(x,y)=>x.ElementAt(y)  , (x,y)=>x.ElementAtOrDefault(y)  },
            {(x,y)=>x.Last()        , (x,y)=>x.LastOrDefault()        },

       };


        public T GetSingle(IOqlCallResult callResult)
        {

            int x, y = 0;

            if ((callResult.Command & OqlCommandToken.DefaultFlag) > 0)
            {
                y++;
                x = (callResult.Command & ~OqlCommandToken.DefaultFlag) - OqlCommandToken.Single;
            }
            else
            {
                x = callResult.Command - OqlCommandToken.Single;
            }

            return m_navigate_getters[x, y](this, callResult.ElementIndex);
        }


        public override object GetResult(IOqlCallResult callResult)
        {

            if(callResult.Command== OqlCommandToken.Select)
            {
                return this as IEnumerable<T>;
            }


            if (callResult.Command == OqlCommandToken.IsAny)
            {
                return this.Any();
            }

            return GetSingle(callResult);
        }

        public abstract IEnumerator<T> GetEnumerator();
       
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

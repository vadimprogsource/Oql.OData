using Microsoft.AspNetCore.Mvc;
using OData.Linq.Api;
using Oql.Linq;
using Oql.Linq.Api.Data;
using Oql.Linq.Api.Query;
using Oql.OData.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Oql.OData.Controllers
{
    [Route("odata")]
    public class ODataController : Controller
    {
        private readonly IObjectQueryProvider provider; 
        private readonly IODataQueryProvider m_query_provider;

        public ODataController(IODataQueryProvider queryProdivder , IObjectQueryProvider provider)
        {
            this.provider = provider;
            m_query_provider = queryProdivder;
        }

        [HttpGet("{entityType}")]
        public Task<IEnumerable> ExecuteQuery(string entityType)
        {

            IQueryable<User> users = provider.CreateQuery<User>();

            User[] usr = users.Where(x => x.Id > 0).ToArray();




            return  m_query_provider
                         .CreateQuery(entityType, WebUtility.UrlDecode(Request.QueryString.ToString()))
                         .AsEnumerableAsync();
                         
        }

      
    }
}

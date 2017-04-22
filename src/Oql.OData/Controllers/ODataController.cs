using Microsoft.AspNetCore.Mvc;
using OData.Linq.Api;
using Oql.Linq;
using Oql.Linq.Api.Data;
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
        private readonly IODataQueryProvider m_query_provider;

        public ODataController(IODataQueryProvider queryProdivder)
        {
            m_query_provider = queryProdivder;
        }

        [HttpGet("{entityType}")]
        public async Task<IEnumerable> ExecuteQuery(string entityType)
        {
            return await m_query_provider
                         .CreateQuery(entityType, WebUtility.UrlDecode(Request.QueryString.ToString()))
                         .AsEnumerableAsync();
        }

      
    }
}

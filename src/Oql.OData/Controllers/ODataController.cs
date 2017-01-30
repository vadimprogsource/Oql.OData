using Microsoft.AspNetCore.Mvc;
using OData.Linq.Api;
using Oql.Linq.Api.Data;
using Oql.OData.Models;
using System;
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
        public IQueryable ExecuteQuery(string entityType)
        {
            return m_query_provider.CreateQuery(entityType, WebUtility.UrlDecode(Request.QueryString.ToString()));
        }

      
    }
}

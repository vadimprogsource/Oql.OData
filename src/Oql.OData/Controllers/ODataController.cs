using Microsoft.AspNetCore.Mvc;
using OData.Linq.Api;
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
        private readonly IODataQueryProvider m_provider;

        public ODataController(IODataQueryProvider provider)
        {
            m_provider = provider;
        }

        [HttpGet("{entityType}")]
        public IQueryable ExecuteQuery(string entityType)
        {
            return m_provider.CreateQuery(entityType, WebUtility.UrlDecode(Request.QueryString.ToString()));
        }
    }
}

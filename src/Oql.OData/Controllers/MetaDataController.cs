using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Data;
using Oql.OData.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Oql.OData.Controllers
{
    [Route("metadata")]
    public class MetaDataController : Controller
    {
        private IMetaDataProvider m_meta_data_provider;

        public MetaDataController(IDataProvider dataProvider)
        {
            m_meta_data_provider = dataProvider.GetMetadata();
        }

        [HttpGet("{entityType}")]
        public object GetMetaData(string entityType)
        {
            return new DtoEntity(m_meta_data_provider.GetEntity(entityType, false));
        }


    }
}

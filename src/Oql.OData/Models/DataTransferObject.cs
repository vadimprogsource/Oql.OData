using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.OData.Models
{
    public class DataTransferObject : DynamicObject
    {

        private IDictionary<string, object> m_this = new Dictionary<string, object>();

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return m_this.Keys.OrderBy(x => x);
        }


        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return m_this.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            m_this[binder.Name] = value;
            return true;
        }

        public object this[string memberName]
        {
            set { m_this[memberName] = value; }
        }
    }
}

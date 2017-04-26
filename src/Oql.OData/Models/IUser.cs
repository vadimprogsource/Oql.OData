using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.OData.Models
{

    public interface IObject
    {
        int Id { get; }
    }
    public interface IClassifier
    {
        string Name { get; }
    }
    public interface User : IObject , IClassifier
    {
    }
}

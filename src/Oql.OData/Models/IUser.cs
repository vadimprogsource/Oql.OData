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
    public interface IUser : IObject , IClassifier
    {
        string Password { get; }
    }
}

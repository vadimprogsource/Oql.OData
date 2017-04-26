using Oql.Linq.Api.Data;
using Oql.Linq.Infrastructure.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.CodeGen
{
    public class EntityConstructorBuilder<TEntity>
    {
        public Func<IDataStruct, TEntity> Build()
        {
            Type runtimeType = typeof(TEntity);

            if (runtimeType.IsAbstract || runtimeType.IsInterface)
            {
               runtimeType =  new EntityCodeBuilder().Build(new Entity(runtimeType));
            }

            DynamicMethod dmBuilder = new DynamicMethod("call.ctor", typeof(TEntity), new[] { typeof(IDataStruct) });
            ILGenerator gen         = dmBuilder.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Newobj  , runtimeType.GetConstructor(new[] { typeof(IDataStruct) }));
            gen.Emit(OpCodes.Ret);

            return dmBuilder.CreateDelegate(typeof(Func<IDataStruct, TEntity>)) as Func<IDataStruct, TEntity>;
        }
    }
}

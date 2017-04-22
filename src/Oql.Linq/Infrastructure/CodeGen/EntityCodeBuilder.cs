using Oql.Linq.Api.Metadata;
using System;

using System.Reflection.Emit;
using System.Linq;
using System.Reflection;
using Oql.Linq.Api.CodeGen;
using Oql.Linq.Api.Data;

namespace Oql.Linq.Infrastructure.CodeGen
{
    public class EntityCodeBuilder : IEntityCodeBuilder
    {

        private AssemblyBuilder m_asm_builder;
        private ModuleBuilder   m_module_builder;


        private static MethodInfo m_get_value_by_name = typeof(IDataStruct).GetMethod("GetValueByName"); 

        public EntityCodeBuilder()
        {

            m_asm_builder    =  AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(GetType().Name + Guid.NewGuid()), AssemblyBuilderAccess.RunAndCollect);
            m_module_builder =    m_asm_builder.DefineDynamicModule  ("module");
        }


        private string GetEntityName(IEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                return "ent_" + Guid.NewGuid().ToString("N");
            }

            return entity.Name;
        }



        private ILGenerator MakeConstructor(TypeBuilder typeBuilder ,  params Type[] args)
        {
            ConstructorBuilder ctBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, args);

            ConstructorInfo based = typeBuilder.BaseType.GetConstructor(new Type[0]);

            ILGenerator gen = ctBuilder.GetILGenerator();

            if (based != null)
            {
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Call, based);

            }

            return gen;

        }



        public Type Build(IEntity entity)
        {

            TypeBuilder typeBuilder = m_module_builder.DefineType(GetEntityName(entity), TypeAttributes.Class | TypeAttributes.Public);

            if (entity.BaseType != null && entity.BaseType != typeof(object))
            {
                if (entity.BaseType.IsInterface)
                {
                    typeBuilder.AddInterfaceImplementation(entity.BaseType);
                }
                else
                {
                    typeBuilder.SetParent(entity.BaseType);
                }
            }

            MakeConstructor(typeBuilder).Emit(OpCodes.Ret);

            ILGenerator gen = MakeConstructor(typeBuilder, new Type[] { typeof(IDataStruct) });

            foreach (IProperty member in entity.Properties)
            {
                FieldBuilder field =   typeBuilder.DefineField("m_"+member.Name.ToLowerInvariant(), member.BaseType, FieldAttributes.Private);


                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldarg_1);
                gen.Emit(OpCodes.Ldstr, member.Name);
                gen.Emit(OpCodes.Call , m_get_value_by_name.MakeGenericMethod(field.FieldType));
                gen.Emit(OpCodes.Stfld , field);



                PropertyBuilder property = typeBuilder.DefineProperty(member.Name, PropertyAttributes.None, member.BaseType , new Type[] {member.BaseType });

                MethodBuilder mdBuilder = typeBuilder.DefineMethod("get_" + member.Name.ToLowerInvariant(), MethodAttributes.Public | MethodAttributes.HideBySig,member.BaseType , new Type[] { } );

                ILGenerator mdGen = mdBuilder.GetILGenerator();
                mdGen.Emit(OpCodes.Ldarg_0);
                mdGen.Emit(OpCodes.Ldfld, field);
                mdGen.Emit(OpCodes.Ret);

                property.SetGetMethod(mdBuilder);

                mdBuilder = typeBuilder.DefineMethod("set_" + member.Name.ToLowerInvariant(), MethodAttributes.Public | MethodAttributes.HideBySig, null, new Type[] { member.BaseType});


                mdGen = mdBuilder.GetILGenerator();
                mdGen.Emit(OpCodes.Ldarg_0);
                mdGen.Emit(OpCodes.Ldarg_1);
                mdGen.Emit(OpCodes.Stfld, field);
                mdGen.Emit(OpCodes.Ret);

                property.SetSetMethod(mdBuilder);
            }

            gen.Emit(OpCodes.Ret);


            return typeBuilder.CreateTypeInfo().AsType();
        }



    }
}

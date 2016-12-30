using System;
using System.Linq;
using Oql.Linq.Api.Metadata;
using System.Linq.Expressions;
using Oql.Linq.Api.Data;
using Oql.Linq.Api.Query;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Infrastructure.Syntax.Formatters;
using System.Collections.Generic;
using Oql.Linq.Api.CodeGen;
using System.Reflection;
using System.Collections;
using Oql.Linq.Infrastructure.CodeGen;

namespace Oql.Linq.Infrastructure.Query
{
    public  class ObjectQueryProvider : IObjectQueryProvider
    {
        private IDataProvider       m_data_provider;
        private ICodeProvider       m_code_provider ;

        public ObjectQueryProvider(IDataProvider dataProvider, ICodeProvider codeProvider)
        {
            m_data_provider = dataProvider;
            m_code_provider = codeProvider;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return Activator.CreateInstance(typeof(ObjectQuery<>).MakeGenericType(expression.Type), this,expression) as IQueryable;
        }



        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new ObjectQuery<TElement>(this, expression);
        }


        public IQueryable CreateQuery(string entityName)
        {
            Type entityType = m_code_provider.GetType(m_data_provider.GetMetadata(), entityName);
            return Activator.CreateInstance(typeof(ObjectQuery<>).MakeGenericType(entityType) ,this ) as IQueryable;
        }

        public IQueryable CreateQuery(IEntityInfo entityInfo)
        {
            Type entityType = m_code_provider.GetType(entityInfo);
            return Activator.CreateInstance(typeof(ObjectQuery<>).MakeGenericType(entityType), this) as IQueryable;
        }

        public IQueryable<TEntity> CreateQuery<TEntity>()
        {
            return new ObjectQuery<TEntity>(this);
        }


        public object Execute(Expression expression)
        {

            IOqlExpressionVisitor visitor = new OqlExpressionVisitor(m_data_provider.CreateSyntaxContext());
            visitor.VisitAndBuild(expression);
            return Activator.CreateInstance(typeof(ObjectQueryIterator<>).MakeGenericType(visitor.ResulType), m_data_provider.GetDataSource(), visitor.Query);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)Execute(expression);
        }

        public IExpressionBuilder CreateExpressionBuilder()
        {
            return new ExpressionBuilder(m_code_provider);
        }
    }
}

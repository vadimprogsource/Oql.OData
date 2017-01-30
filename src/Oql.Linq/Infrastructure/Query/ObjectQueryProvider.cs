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
using Oql.Linq.Infrastructure.Query.Processors;
using Oql.Linq.Api;

namespace Oql.Linq.Infrastructure.Query
{
    public  class ObjectQueryProvider : IObjectQueryProvider , IExtent
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

        public IQueryable CreateQuery(IEntity entityInfo)
        {
            Type entityType = m_code_provider.GetType(entityInfo);
            return Activator.CreateInstance(typeof(ObjectQuery<>).MakeGenericType(entityType), this) as IQueryable;
        }

        public IQueryable<TEntity> CreateQuery<TEntity>()
        {
            return new ObjectQuery<TEntity>(this);
        }


        private int ExecuteCommand(IOqlExpressionVisitor visitor)
        {
            using (IDataSource dataSource = m_data_provider.GetDataSource())
            {
                return dataSource.ExecuteCommand(visitor.Query);
            }
        }

        private object GetScalar(IOqlExpressionVisitor visitor)
        {
            using (IDataSource dataSource = m_data_provider.GetDataSource())
            {
                return Convert.ChangeType(dataSource.GetScalar(visitor.Query), visitor.Context.CallResult.ResultType);
            }
        }

        public object Execute(Expression expression)
        {
            IOqlExpressionVisitor visitor = m_data_provider.CreateExpressionVisitor().ExecuteVisit(expression);

            switch (visitor.Context.CallResult.Command)
            {
                case OqlCommandToken.Exec  : return ExecuteCommand(visitor);
                case OqlCommandToken.Scalar: return GetScalar(visitor);
                default:
                    return ObjectQueryNavigator.CreateResult(visitor.Context.CallResult, m_data_provider.GetDataSource(), visitor.Query);
            }
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)Execute(expression);
        }

        public IExpressionBuilder CreateExpressionBuilder()
        {
            return new ExpressionBuilder(m_code_provider);
        }

        public IObjectQueryProcessor<TEntity> CreateQueryProcessor<TEntity>(IQueryable<TEntity> queryable)
        {
            return new ObjectQueryProcessor<TEntity>(queryable);
        }

        public IQueryable<TEntity> AsQueryable<TEntity>()
        {
            return CreateQuery<TEntity>();
        }
    }
}

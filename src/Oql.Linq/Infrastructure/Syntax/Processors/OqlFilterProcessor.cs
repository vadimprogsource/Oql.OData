using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Syntax.Processors
{
    public class OqlFilterProcessor
    {


        private ConstantExpression GetConstant(IExpression x, Type t)
        {
            return Expression.Constant(Convert.ChangeType(x.Value, t), t);
        }


        private ExpressionType GetComparison(IExpression x)
        {

            if (x.Eq)
            {
                return  ExpressionType.Equal;
            }

            if (x.Ne)
            {
                return ExpressionType.NotEqual;
            }

            if (x.Gt)
            {
                return ExpressionType.GreaterThan;
            }

            if (x.Ge)
            {
                return ExpressionType.GreaterThanOrEqual;
            }


            if (x.Lt)
            {
                return ExpressionType.GreaterThan;
            }

            if (x.Le)
            {
                return ExpressionType.GreaterThanOrEqual;
            }

            return ExpressionType.Equal;
        }


        public IQueryable<T> ProcessFilterBy<T>(IQueryable<T> query, IExpression expression)
        {


            ParameterExpression param = Expression.Parameter(typeof(T), "x");


            Expression body = null;

            for (IExpression x = expression; x != null; x = x.Expression)
            {
                ExpressionType lg = ExpressionType.And;

                if (x.And)
                {
                    lg = ExpressionType.AndAlso;
                    x = x.Expression;
                }
                else
                if (x.Or)
                {
                    lg = ExpressionType.OrElse;
                    x = x.Expression;
                }


                bool is_not = false;

                if (x.Not)
                {
                    is_not = true;
                    x = x.Expression;
                }


                Expression         left  = Expression.PropertyOrField(param, x.Name);
                ExpressionType     op    = GetComparison(x = x.Expression);
                ConstantExpression right = GetConstant  (x = x.Expression,left.Type);

                left = Expression.MakeBinary(op, left, right);

                if (is_not)
                {
                    left = Expression.Not(left);
                }

                if (body == null)
                {
                    body = left;
                    continue;
                }


                body = Expression.MakeBinary(lg , body ,left);
            }

            return query.Where(Expression.Lambda<Func<T, bool>>(body, param));
        }
    }
}

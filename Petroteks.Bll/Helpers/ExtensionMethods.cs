using System;
using System.Linq;
using System.Linq.Expressions;

namespace Petroteks.Bll.Helpers
{
    public static class ExtensionMethods
    {

    }

    public static class ExpressionCombiner
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> newExp)
        {
            ParameterUpdateVisitor visitor = new ParameterUpdateVisitor(newExp.Parameters.First(), exp.Parameters.First());
            newExp = visitor.Visit(newExp) as Expression<Func<T, bool>>;
            BinaryExpression binExp = Expression.And(exp.Body, newExp.Body);
            return Expression.Lambda<Func<T, bool>>(binExp, newExp.Parameters);
        }

        private class ParameterUpdateVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (object.ReferenceEquals(node, _oldParameter))
                {
                    return _newParameter;
                }

                return base.VisitParameter(node);
            }
        }
    }
}

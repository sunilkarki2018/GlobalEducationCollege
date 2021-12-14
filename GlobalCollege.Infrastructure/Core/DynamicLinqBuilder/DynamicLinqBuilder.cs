using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Infrastructure
{
    public static class DynamicLinqBuilder
    {
        public static Expression<Func<T, S>> CreateExpression<T, S>(string expression, params object[] values)
        {
            var lambdaExpression =
               System.Linq.Dynamic.DynamicExpression.ParseLambda<T, S>(expression, values);
            return lambdaExpression;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilterProvider.Visitors
{
    public class ParameterVisitor : ExpressionVisitor
    {
        private ParameterExpression Parameter { get; set; }
        public static Expression BuildBinary(Expression expression, ParameterExpression parameter)
        {
            ParameterVisitor transform = new ParameterVisitor();
            transform.Parameter = parameter;
            return transform.Visit(expression);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return this.Parameter;
        }
    }
}

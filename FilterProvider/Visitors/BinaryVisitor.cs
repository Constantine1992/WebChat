using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilterProvider.Visitors
{
    public class BinaryVisitor : ExpressionVisitor
    {
        private Expression Filter { get; set; }
        public static Expression BuildBinary(Expression expression)
        {
            BinaryVisitor transform = new BinaryVisitor();
            transform.Visit(expression);
            return transform.Filter;
        }
        protected override Expression VisitBinary(BinaryExpression binaryExpression)
        {
            this.Filter = binaryExpression;
            return binaryExpression;
        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            this.Filter = node;
            return node;
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            this.Filter = node;
            return node;
        }
    }
}

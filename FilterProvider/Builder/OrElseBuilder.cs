using FilterProvider.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilterProvider.Builder
{
    public class OrElseBuilder<TResult>
    {
        private ParameterExpression Parameter { get; set; }
        private Expression<Func<TResult, bool>> Lambda { get; set; }
        private OrElseBuilder() { }
        public static OrElseBuilder<TResult> CreateBuilder(Expression<Func<TResult, bool>> filter)
        {
            if (filter == null)
                throw new Exception("Последовательность пустая");
            OrElseBuilder<TResult> builder = new OrElseBuilder<TResult>();
            builder.SetValueForLambda(filter);
            return builder;
        }
        public static OrElseBuilder<TResult> CreateBuilder()
        {
            OrElseBuilder<TResult> builder = new OrElseBuilder<TResult>();
            return builder;
        }
        public void AppendAndAlso(Expression<Func<TResult, bool>> filter, bool condition = true)
        {
            AppendNode(filter, ExpressionType.AndAlso, condition);
        }
        public void AppendOrElse(Expression<Func<TResult, bool>> filter, bool condition = true)
        {
            AppendNode(filter, ExpressionType.OrElse, condition);
        }
        private void AppendNode(Expression<Func<TResult, bool>> filter, ExpressionType logicType, bool condition)
        {
            if (!condition)
                return;
            if (this.Lambda == null)
            {
                this.SetValueForLambda(filter);
                return;
            }
            Expression lambdaBinary = BinaryVisitor.BuildBinary(this.Lambda);
            Expression filterLambda = ParameterVisitor.BuildBinary(filter, this.Parameter);
            Expression filterBinary = BinaryVisitor.BuildBinary(filterLambda);

            BinaryExpression resultBinary = Expression.MakeBinary(logicType, lambdaBinary, filterBinary);
            Expression<Func<TResult, bool>> resultLambda = Expression.Lambda<Func<TResult, bool>>(resultBinary, this.Parameter);
            this.Lambda = resultLambda;
        }
        private void SetValueForLambda(Expression<Func<TResult, bool>> filter)
        {
            this.Parameter = filter.Parameters[0];
            this.Lambda = filter;
        }
        public Expression<Func<TResult, bool>> GetFilter()
        {
            if (this.Lambda == null)
            {
                this.Lambda = (n => true);
            }
            return this.Lambda;
        }
    }
}

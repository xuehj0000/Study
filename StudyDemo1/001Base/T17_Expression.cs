using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace StudyDemo1._001Base
{
    public class T17_Expression
    {
    }

    

    public class OperationsVisitor: ExpressionVisitor
    {
        private Stack<string> _sqlStack = new Stack<string>();

        public string Condition()
        {
            var condition = string.Concat(this._sqlStack.ToArray());
            this._sqlStack.Clear();
            return condition;
        }

        /// <summary>
        /// 解析二元表达式
        /// </summary>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node == null) throw new Exception("BinaryExpression 异常！");

            _sqlStack.Push(")");
            base.Visit(node.Right); //解析右边
            _sqlStack.Push(" " + node.NodeType.ToSqlOperator() + " ");
            base.Visit(node.Left);  //解析左边
            _sqlStack.Push("(");
            return node;
        }

        /// <summary>
        /// 解析常量表达式
        /// </summary>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node == null) throw new Exception("ConstantExpression 异常！");
            _sqlStack.Push($" '{node.Value}' ");
            return node;
        }



        public Expression Modify(Expression expression)
        {
            return this.Visit(expression);
        }
      

        
    }

    internal static class SqlOperator
    {
        /// <summary>
        /// <remarks>解析操作类型</remarks>
        /// </summary>
        /// <param name="expressionType">操作类型：例如（>,=）</param>
        /// <returns></returns>
        internal static string ToSqlOperator(this ExpressionType expressionType)
        {
            // 将操作类型解析为对应的表达方式
            switch (expressionType)
            {
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Not:
                    return "NOT";
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return "OR";
                default:
                    throw new Exception($"不支持 {expressionType.ToString()} 方法!");
            }
        }
    }
}

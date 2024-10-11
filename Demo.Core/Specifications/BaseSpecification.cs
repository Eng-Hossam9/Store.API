using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Specifications
{
    public class BaseSpecification<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Conditions { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> Orderby { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderbyDesc { get ; set ; } = null;
        public int? Take { get ; set; }
        public int? Skip { get ; set ; }
        public bool IsPagination { get; set; } = false;

        public BaseSpecification(Expression<Func<TEntity, bool>> conditions)
        {
            Conditions= conditions;

        }
        public BaseSpecification()
        {

        }

        public void AddOrderByAsc(Expression<Func<TEntity, object>> expression)
        {
            Orderby= expression;

        }
        public void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderbyDesc = expression;

        }

        public void ApplyPagination(int? skip , int? take)
        {
            IsPagination = true;
            Skip = skip;
            Take = take;

        }



    }
}

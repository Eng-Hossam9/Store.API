using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Specifications
{
    public interface ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Conditions { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }

        public Expression<Func<TEntity, object>> Orderby { get; set; }

        public Expression<Func<TEntity, object>> OrderbyDesc { get; set; }

        public int? Take { get; set; }
        public int? Skip { get; set; }
        public bool IsPagination { get; set; }



    }
}

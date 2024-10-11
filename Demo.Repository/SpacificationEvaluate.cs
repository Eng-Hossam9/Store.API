using Demo.Core.Models;
using Demo.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository
{
    public static class SpacificationEvaluate<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        public static IQueryable<TEntity> GenerateQuery(IQueryable<TEntity> InputQuery,ISpecifications<TEntity, Tkey> spec)
        {
            var Query = InputQuery;

            if (spec.Conditions is not null) 
            {
                Query= Query.Where(spec.Conditions);
            }

            if (spec.Orderby is not null) 
            {
                Query = Query.OrderBy(spec.Orderby);
            }

            if (spec.OrderbyDesc is not null)
            {
                Query = Query.OrderByDescending(spec.OrderbyDesc);
            }
            if (spec.IsPagination)
            {
                Query=Query.Skip(spec.Skip.Value).Take(spec.Take.Value);
            }
            if (spec.Includes.Count > 0)
            {
                Query = spec.Includes.Aggregate(Query, (CurrentQuery, QueryExpression) => CurrentQuery.Include(QueryExpression));
            }



            return Query;
        }
    }
}

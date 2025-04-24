using Microsoft.EntityFrameworkCore;

namespace SportsPro.Models.DataLayer
{
    public static class QueryExtensions
    {

        public static IQueryable<T> Apply<T>(this IQueryable<T> query, QueryOptions<T> options) where T : class
        {
            //  includes
            foreach (var include in options.GetIncludes())
                query = query.Include(include);

            //  where clauses
            if (options.HasWhere)
            {
                foreach (var clause in options.WhereClauses)
                    query = query.Where(clause);
            }

            //  ordering
            if (options.HasOrderBy)
            {
                if (options.OrderByDirection.ToLower() == "desc")
                {
                    query = query.OrderByDescending(options.OrderBy);
                }
                else
                {
                    query = query.OrderBy(options.OrderBy);
                }

                if (options.HasThenOrderBy)
                {
                    query = ((IOrderedQueryable<T>)query)
                        .ThenBy(options.ThenOrderBy);
                }
            }


            return query;
        }
    }
}

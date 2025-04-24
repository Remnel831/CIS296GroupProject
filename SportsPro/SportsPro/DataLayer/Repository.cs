using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.DataLayer
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected SportsProContext Context { get; set; }
        private DbSet<T> Dbset { get; set; }

        public Repository(SportsProContext ctx)
        {
            Context = ctx;
            Dbset = Context.Set<T>();
        }

        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = Dbset;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
                query = query.Where(options.Where);
            if (options.HasOrderBy)
            {
                if (options.HasThenOrderBy)
                {
                    query = query.OrderBy(options.OrderBy).ThenBy(options.ThenOrderBy);
                }
                else
                {
                    query = query.OrderBy(options.OrderBy);
                }
            }
            return query.ToList();
        }

        public virtual T? Get(int id) => Dbset.Find(id);
        public virtual T? Get(QueryOptions<T> options)
        {
            IQueryable<T> query = Dbset;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
                query = query.Where(options.Where);
            return query.FirstOrDefault();
        }
        public virtual void Insert(T entity) => Dbset.Add(entity);
        public virtual void Update(T entity) => Dbset.Update(entity);
        public virtual void Delete(T entity) => Dbset.Remove(entity);
        public virtual void Save() => Context.SaveChanges();
    }

}

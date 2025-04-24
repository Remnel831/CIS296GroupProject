using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SportsPro.Models.DataLayer
{
    public class QueryOptions<T>
    {
        public List<Expression<Func<T, bool>>> WhereClauses { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> ThenOrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc";

        private string[] includes;
        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',');
        }

        public string[] GetIncludes() => includes ?? Array.Empty<string>();

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }

        public bool HasWhere => WhereClauses.Count > 0;
        public bool HasOrderBy => OrderBy != null;
        public bool HasThenOrderBy => ThenOrderBy != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;


        public Expression<Func<T, bool>> Where
        {
            get => WhereClauses.FirstOrDefault();
            set => WhereClauses.Add(value);
        }
    }
}


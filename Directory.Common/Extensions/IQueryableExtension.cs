namespace Directory.Common.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class Extensions
    {
        private const string Asc = "OrderByProperty";
        private const string Desc = "OrderByPropertyDescending";

        //public static IQueryable<T> Sort<T>(this IQueryable<T> query, PageSortInfo pageSort)
        //{
        //    if (String.IsNullOrWhiteSpace(pageSort.SortField))
        //        return query;


        //    var p = query.First().GetType().GetProperty(pageSort.SortField);

        //    if (pageSort.SortOrder == SortOrderEnum.Asc)
        //        return query.OrderBy(s => s.GetType()
        //                                   .GetProperty(pageSort.SortField));

        //    return query.OrderByDescending(s => s.GetType()
        //                                         .GetProperty(pageSort.SortField));
        //}

        public static IQueryable<TModel> Sort<TModel>(this IQueryable<TModel> q, PageSortInfo pageSort)
        {
            string orderDist = pageSort.SortOrder != null && pageSort.SortOrder.Value == SortOrderEnum.Asc ? Asc : Desc;
            Type entityType = typeof(TModel);
            PropertyInfo p = entityType.GetProperty(pageSort.SortField);

            MethodInfo m = typeof(Extensions).GetMethod(orderDist).MakeGenericMethod(entityType, p.PropertyType);
            return (IQueryable<TModel>)m.Invoke(null, new object[] { q, p });
        }

        public static IQueryable<TModel> OrderByPropertyDescending<TModel, TRet>(IQueryable<TModel> q, PropertyInfo p)
        {
            ParameterExpression pe = Expression.Parameter(typeof(TModel));
            Expression se = Expression.Convert(Expression.Property(pe, p), typeof(TRet));
            return q.OrderByDescending(Expression.Lambda<Func<TModel, TRet>>(se, pe));
        }

        public static IQueryable<TModel> OrderByProperty<TModel, TRet>(IQueryable<TModel> q, PropertyInfo p)
        {
            ParameterExpression pe = Expression.Parameter(typeof(TModel));
            Expression se = Expression.Convert(Expression.Property(pe, p), typeof(TRet));
            return q.OrderBy(Expression.Lambda<Func<TModel, TRet>>(se, pe));
        }
    }
}

using System.Linq.Expressions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestEntityFramework.ApplicationModels;

namespace TestEntityFramework.Extensions;

public static class QueryableSortExtension
{
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, List<SortItem> sortItems)
    {
        IOrderedQueryable<T> orderedQuery = null;

        for (int i = 0; i < sortItems.Count; i++)
        {
            var order = sortItems[i];
            var parameter = Expression.Parameter(typeof(T), "x");
            var selector = Expression.PropertyOrField(parameter, order.PropertyName);
            var lambda = Expression.Lambda(selector, parameter);

            if (i == 0)
            {
                orderedQuery = order.Direction == SortDirection.Ascending
                    ? Queryable.OrderBy(source, (dynamic)lambda)
                    : Queryable.OrderByDescending(source, (dynamic)lambda);
            }
            else
            {
                orderedQuery = order.Direction == SortDirection.Ascending
                    ? Queryable.ThenBy((dynamic)orderedQuery, (dynamic)lambda)
                    : Queryable.ThenByDescending((dynamic)orderedQuery, (dynamic)lambda);
            }
        }

        return orderedQuery ?? source;
    }
}
using System.Diagnostics;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T>
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        if (specification.OrderByDesc != null)
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        if (specification.IsDistinct)
        {
            query = query.Distinct();
        }

        query = query.Skip(specification.PageSize * specification.PageNumber).Take(specification.PageSize);

        return query;
    }

    public static IQueryable<TResult> GetQuery<TResult>(IQueryable<T> query, ISpecification<T, TResult> specification)
    {
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        if (specification.OrderByDesc != null)
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        var selectQuery = query as IQueryable<TResult>;

        if (specification.Select != null)
        {
            selectQuery = query.Select(specification.Select);
        }

        if (specification.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }

        selectQuery = selectQuery?.Skip(specification.PageSize * specification.PageNumber).Take(specification.PageSize);

        return selectQuery ?? query.Cast<TResult>();
    }
}

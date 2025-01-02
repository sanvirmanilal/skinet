using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification() : this(null) { }

    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDesc { get; private set; }
    public bool IsDistinct { get; private set; }

    public int PageNumber { get; private set; }

    public int PageSize { get; private set; }

    protected void SetOrderBy(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }

    protected void SetOrderByDesc(Expression<Func<T, object>> orderByDesc)
    {
        OrderByDesc = orderByDesc;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }

    protected void SetPageNumber(int? pageNumber)
    {
        if (pageNumber != null)
        {
            PageNumber = pageNumber.Value;
        }
        else
        {
            PageNumber = default;
        }
    }

    protected void SetPageSize(int? pageSize)
    {
        if (pageSize != null)
        {
            PageSize = pageSize.Value;
        }
        else
        {
            PageSize = default;
        }
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    public Expression<Func<T, TResult>>? Select { get; private set; }
    protected void SetSelect(Expression<Func<T, TResult>> select)
    {
        Select = select;
    }
}
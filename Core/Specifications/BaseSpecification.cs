using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification() : this(null) { }

    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDesc { get; private set; }

    protected void SetOrderBy(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }

    protected void SetOrderByDesc(Expression<Func<T, object>> orderByDesc)
    {
        OrderByDesc = orderByDesc;
    }
}
using Core.Entities;

namespace API.DTOs;

public class Pagination<T> where T : BaseEntity
{
    public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> items)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Items = items;
    }

    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int Count { get; init; }
    public IReadOnlyList<T>? Items { get; init; }
}
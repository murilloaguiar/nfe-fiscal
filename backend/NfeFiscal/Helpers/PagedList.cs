namespace NfeFiscal.Helpers;

public class PagedList<T>
{
    public PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    public List<T> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => PageSize > 1;
    public int From => (Page - 1) * PageSize + 1;
    public int To => Math.Min(Page * PageSize, TotalCount);

    public static PagedList<T> Create(IEnumerable<T> objects, int page, int pageSize)
    {
        var totalCount = objects.Count();
        var items = objects.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new(items, page, pageSize, totalCount);
    }
}

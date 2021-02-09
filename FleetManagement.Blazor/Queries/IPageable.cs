namespace FleetManagement.Blazor.Queries
{
    public interface IPageable
    {
        public int Page { get; }
        public int PageSize { get; }
    }
}

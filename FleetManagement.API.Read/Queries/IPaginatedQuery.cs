namespace FleetManagement.API.Read.Queries
{
    public interface IPaginatedQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

namespace FleetManagement.API.Read.Queries
{
    public interface ISortableQuery
    {
        public string PropertyName { get; set; }
        public bool Descending { get; set; }
    }
}

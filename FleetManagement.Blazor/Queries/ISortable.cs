namespace FleetManagement.Blazor.Queries
{
    public interface ISortable
    {
        public string PropertyName { get; set; }
        public bool Descending { get; set; }
    }
}

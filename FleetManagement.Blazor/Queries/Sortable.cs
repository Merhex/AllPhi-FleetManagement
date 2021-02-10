namespace FleetManagement.Blazor.Queries
{
    public class Sortable : ISortable
    {
        public string PropertyName { get; set; }
        public bool Descending { get; set; }
    }
}

namespace FleetManagement.WinForms.Queries
{
    public class DriversQuery : IQuery, IPageable
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public string Endpoint 
        {
            get 
            {
                IPageable pageable = this;

                return $"Drivers?{pageable.GetPaginationQueryString()}";
            } 
        }
    }
}

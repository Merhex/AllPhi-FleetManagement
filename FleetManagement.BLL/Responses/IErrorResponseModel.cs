namespace FleetManagement.BLL
{
    public interface IErrorResponseModel
    {
        public string Type { get; set; }
        public string Field { get; set; }
        public string Error { get; set; }
    }
}

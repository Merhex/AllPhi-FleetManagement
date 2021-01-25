namespace FleetManagement.BLL
{
    public class ErrorResponseModel : IErrorResponseModel
    {
        public string Type { get; set; }
        public string Field { get; set; }
        public string Error { get; set; }
    }
}

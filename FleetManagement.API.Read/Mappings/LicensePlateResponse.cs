namespace FleetManagement.API.Read.Mappings
{
    public record LicensePlateResponse
    (
        string Identifier,
        bool InUse
    );
}

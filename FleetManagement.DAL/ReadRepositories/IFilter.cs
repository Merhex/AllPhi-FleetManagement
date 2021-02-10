namespace FleetManagement.DAL.Repositories.Interfaces
{
    public interface IFilter
    {
        bool Satisfy<T>(T item);
    }
}

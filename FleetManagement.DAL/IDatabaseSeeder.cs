using System.Threading.Tasks;

namespace FleetManagement.DAL
{
    public interface IDatabaseSeeder
    {
        /// <summary>
        /// Adds some default values to the Database
        /// </summary>
        Task SeedDatabase();
    }
}

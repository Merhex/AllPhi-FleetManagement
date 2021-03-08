using System.Threading.Tasks;

namespace FleetManagement.DAL.DatabaseSeeding
{
    public interface IDatabaseSeeder
    {
        /// <summary>
        /// Adds some default values to the Database
        /// </summary>
        Task SeedDatabase();
    }
}

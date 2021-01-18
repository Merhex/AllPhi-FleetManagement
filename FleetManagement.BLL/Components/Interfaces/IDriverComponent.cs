using FleetManagement.BLL.Commands;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components.Interfaces
{
    public interface IDriverComponent
    {
        Task CreateDriverAsync(CreateDriverCommand command);
    }
}

using FleetManagement.BLL.MotorVehicles.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagement.BLL
{
    public static class BusinessRequirementsRegistrations
    {

        public static IServiceCollection AddBusinessRequirements(this IServiceCollection collection)
        {
            collection.AddTransient<IBusinessRequirements<ICreateMotorVehicleContract>, CreateMotorVehicleRequirements>();
            collection.AddTransient<IBusinessRequirements<ICreateLicensePlateContract>, CreateLicensePlateRequirements>();
            collection.AddTransient<IBusinessRequirements<IAssignLicensePlateContract>, AssignLicensePlateRequirements>();
            collection.AddTransient<IBusinessRequirements<IDeleteLicensePlateContract>, DeleteLicensePlateRequirements>();
            collection.AddTransient<IBusinessRequirements<IWithdrawLicensePlateContract>, WithdrawLicensePlateRequirements>();
            collection.AddTransient<IBusinessRequirements<IActivateMotorVehicle>, ActivateMotorVehicleRequirements>();
            collection.AddTransient<IBusinessRequirements<IDeactivateMotorVehicle>, DeactivateMotorVehicleRequirements>();
            collection.AddTransient<IBusinessRequirements<IActivateLicensePlateContract>, ActivateLicensePlateRequirements>();
            collection.AddTransient<IBusinessRequirements<IDeactivateLicensePlateContract>, DeactivateLicensePlateRequirements>();

            return collection;
        }
    }
}

using FleetManagement.BLL.Drivers.Contracts;
using FleetManagement.BLL.Drivers.Contracts.Requirements;
using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.BLL.Persons.Contracts;
using FleetManagement.BLL.Persons.Contracts.Requirements;
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
            collection.AddTransient<IBusinessRequirements<IActivateMotorVehicleContract>, ActivateMotorVehicleRequirements>();
            collection.AddTransient<IBusinessRequirements<IDeactivateMotorVehicleContract>, DeactivateMotorVehicleRequirements>();
            collection.AddTransient<IBusinessRequirements<IActivateLicensePlateContract>, ActivateLicensePlateRequirements>();
            collection.AddTransient<IBusinessRequirements<IDeactivateLicensePlateContract>, DeactivateLicensePlateRequirements>();
            collection.AddTransient<IBusinessRequirements<IUpdateMotorVehicleContract>, UpdateMotorVehicleRequirements>();
            collection.AddTransient<IBusinessRequirements<ICreateDriverContract>, CreateDriverRequirements>();
            collection.AddTransient<IBusinessRequirements<IActivateDriverContract>, ActivateDriverRequirements>();
            collection.AddTransient<IBusinessRequirements<IDeactivateDriverContract>, DeactivateDriverRequirments>();
            collection.AddTransient<IBusinessRequirements<IUpdatePersonInformationContract>, UpdatePersonInformationRequirements>();

            return collection;
        }
    }
}

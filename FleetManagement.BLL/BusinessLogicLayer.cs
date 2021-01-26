using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    /// <summary>
    /// The BLL assembly handle, for the registrations in the service container.
    /// </summary>
    public class BusinessLogicLayer { }


    public class BusinessHandler 
    {
        public delegate Task Requirements();
    }

    public class BusinessRuleListener 
    {

    }

    public abstract class DoesNotExistsBusinessRule<T> : BusinessRule<T> where T : IContract 
    {

    }

    public class LicensePlateDoesNotExistBusinessRule : DoesNotExistsBusinessRule<ICreateLicensePlateContract>
    {
        private readonly ILicensePlateRepository _licensePlateRepository;

        public LicensePlateDoesNotExistBusinessRule(ILicensePlateRepository licensePlateRepository)
        {
            _licensePlateRepository = licensePlateRepository;
        }

        public async override Task Handle(ICreateLicensePlateContract contract)
        {
            
        }
    }

    public abstract class BusinessRule<T> where T : IContract
    {
        public abstract Task Handle(T contract);
    }

    public interface IContract
    {

    }
}

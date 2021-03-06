﻿using System;

namespace FleetManagement.BLL.Persons.Contracts
{
    public interface IUpdatePersonInformationContract : IContract
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string NationalNumber { get; init; }
        public string AddressLine { get; init; }
        public string City { get; init; }
        public int ZipCode { get; init; }
    }
}

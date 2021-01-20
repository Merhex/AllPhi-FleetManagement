﻿using FleetManagement.BLL.Commands;
using FleetManagement.BLL.Validators.Interfaces;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using FluentValidation;
using System;

namespace FleetManagement.BLL.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator(IBelgianNationalNumberValidator belgianNationalNumberValidator)
        {
            RuleFor(person => person.NationalNumber)
                .Must((person, nationalNumber) => belgianNationalNumberValidator
                .Validate(person.DateOfBirth, nationalNumber))
                .WithMessage("The supplied national number is not valid. Checksum failed, check given numbers and birth date.");

            RuleFor(person => person.NationalNumber)
                .Must((person, nationalNumber) => belgianNationalNumberValidator
                .ValidateFormat(nationalNumber))
                .WithMessage("The supplied national number is not valid. Formatting is incorrect.");

            RuleFor(person => person.FirstName)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(person => person.LastName)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(person => person.City)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(person => person.Street)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(person => person.ZipCode)
                .InclusiveBetween(1000, 9999)
                .NotEmpty();
        }
    }
}
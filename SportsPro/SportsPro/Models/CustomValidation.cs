﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Models
{
    public class ValidCountry : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Country is required.");
            }

            var dbContext = (SportsProContext)validationContext.GetService(typeof(SportsProContext));

            //converts the input into a string and then capitalizes it to make sure it matches the table.
            //Since the user is limited to a dropdown list this shouldn't be needed, but it doesn't hurt.
            var countryCode = value.ToString().ToUpper();

            if(!dbContext.Countries.Any(c => c.CountryID == countryCode))
            {
                return new ValidationResult($"{countryCode} is not valid.");
            }

            return ValidationResult.Success;
        }
    }

    public class NewEmail : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Email is required.");
            }
            var dbContext = (SportsProContext)validationContext.GetService(typeof(SportsProContext));

            //The email should come in as a string already, but this will make sure of it.
            var email = value.ToString();

            //Stops the custom validation from returning an error when editing an existing customer.
            //new customers all have an ID of 0 when they're first being created for validation checks so if they have an ID > 0 they can't be new.
            var customerID = validationContext.ObjectType.GetProperty("CustomerID");
            var id = (int?)customerID?.GetValue(validationContext.ObjectInstance);

            if (id > 0)
            {
                return ValidationResult.Success;
            }
			if (dbContext.Customers.Any(c => c.Email == email))
            {
                return new ValidationResult($"{email} is already in use.");
            }

            return ValidationResult.Success;
        }
    }
}

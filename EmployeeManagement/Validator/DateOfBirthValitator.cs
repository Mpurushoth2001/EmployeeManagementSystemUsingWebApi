﻿using FluentValidation.Validators;

namespace EmployeeManagement.Validator
{
    public class DateOfBirthValitator : PropertyValidator
    {
        public DateOfBirthValitator():base("Invalid Date Of Birth")
        {
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            //Allows Age under 60
            DateTime date = (DateTime)context.PropertyValue;
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            //Checks The Value Is null
            if (context.PropertyValue!=null)
            {
                if (dobYear <= currentYear && dobYear > currentYear - 60 && dobYear!=currentYear)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

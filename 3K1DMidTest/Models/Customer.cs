using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _3K1DMidTest.Models;

public partial class Customer
{
    [DisplayName("Customer Id")]
    public int Id { get; set; }

    [DisplayName("Customer Name")]
    public string Name { get; set; } = null!;

    [DisplayName("Customer Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [MinAge(18, ErrorMessage = "Customer must be at least 18 years old.")]
    public DateTime BirthDate { get; set; }

    [DisplayName("Is A Young Driver?")]
    public bool IsYoungDriver { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}

public class MinAgeAttribute : ValidationAttribute
{
    private readonly int _minAge;

    public MinAgeAttribute(int minAge)
    {
        _minAge = minAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            if (age < _minAge)
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}

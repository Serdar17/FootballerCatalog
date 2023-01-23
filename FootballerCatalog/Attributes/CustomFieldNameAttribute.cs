using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Web.Models.Repository.Interfaces;

namespace Web.Attributes;

public class CustomFieldNameAttribute : ValidationAttribute
{
        public override bool IsValid(object? value)
    {
        if (value is null)
            return false;
        var pattern = @"^[a-zA-Zа-яА-ЯёЁ]*$";
        if (Regex.IsMatch((string) value, pattern))
            return true;
        return false;
    }
}
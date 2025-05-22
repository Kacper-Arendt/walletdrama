using System.ComponentModel.DataAnnotations;
using Teams.Domain.Const;

namespace Teams.Core.Commands.Invitations.Attributes;

public class NotAdminRoleAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        return value is TeamRole.Admin
            ? new ValidationResult("Admin role cannot be assigned via invitation.")
            : ValidationResult.Success;
    }
}
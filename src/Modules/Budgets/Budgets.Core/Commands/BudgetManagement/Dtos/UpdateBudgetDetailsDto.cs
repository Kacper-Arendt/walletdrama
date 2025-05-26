using System.ComponentModel.DataAnnotations;
using Budgets.Domain.ValueObjects;

namespace Budgets.Core.Commands.BudgetManagement.Dtos;

public class UpdateBudgetDetailsDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(255, MinimumLength = 3)]
    public string Name { get; set; }
    
    [StringLength(1000)]
    public string Description { get; set; } = "";
}
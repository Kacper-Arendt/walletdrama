using System.ComponentModel.DataAnnotations;

namespace Budgets.Core.Commands.BudgetManagement.Dtos;

public class CreateBudgetDto
{
    [Required]
    [StringLength(255, MinimumLength = 3)]
    public string Name { get; set; }
    
    [StringLength(1000)]
    public string Description { get; set; } = "";
}
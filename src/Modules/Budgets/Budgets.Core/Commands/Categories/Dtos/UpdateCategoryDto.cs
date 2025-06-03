using System.ComponentModel.DataAnnotations;
using Budgets.Domain.Enums;

namespace Budgets.Core.Commands.Categories.Dtos;

public class UpdateCategoryDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }
    
    [StringLength(255)]
    public string Description { get; set; } = "";
    
    [Required]
    public Guid BudgetId { get; set; }
    
    [Required]
    public bool IsActive { get; set; } = true;
    
    [Required]
    public TransactionType Type { get; set; }
}
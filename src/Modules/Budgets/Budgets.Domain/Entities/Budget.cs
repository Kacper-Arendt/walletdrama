using Budgets.Domain.ValueObjects;
using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.Entities;

public class Budget
{
    public BudgetId Id { get; init; }
    public UserId OwnerId { get; init; }

    public BudgetDetails Details { get; init; }
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    public IEnumerable<BudgetYear> Years { get; set; } = new List<BudgetYear>();

    private Budget()
    {
    }

    public Budget(BudgetId id, UserId ownerId, BudgetDetails details)
    {
        Id = id;
        OwnerId = ownerId;
        Details = details;
    }

    public void UpdateDetails(BudgetName name, string description)
    {
        Details.Name = name;
        Details.Description = description;
    }
    
    public void AddYear(BudgetYear year)
    {
        if (Years.Any(y => y.Year.Equals(year.Year)))
            throw new InvalidOperationException($"Year {year.Year} already exists in the budget {Details.Name}.");

        Years = Years.Append(year);
    }
    
    public void RemoveYear(BudgetYear year)
    {
        if (!Years.Any(y => y.Year.Equals(year.Year)))
            throw new InvalidOperationException($"Year {year.Year} does not exist in the budget {Details.Name}.");

        Years = Years.Where(y => !y.Year.Equals(year.Year));
    }
    
    public BudgetYear? GetYear(Year year)
        => Years.FirstOrDefault(y => y.Year.Equals(year));
    
    public IEnumerable<BudgetYear> GetAllYears()
        => Years;
}
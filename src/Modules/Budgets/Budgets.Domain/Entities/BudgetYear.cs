using Budgets.Domain.ValueObjects;

namespace Budgets.Domain.Entities;

public class BudgetYear
{
    public YearId Id { get; init; }
    public Year Year { get; init; }
    public BudgetId BudgetId { get; init; }
    public List<BudgetMonth> Months { get; set; } = [];
    
    private BudgetYear()
    {
    }
    
    public BudgetYear(YearId id, Year year, BudgetId budgetId)
    {
        Id = id;
        Year = year;
        BudgetId = budgetId;
    }

    public void AddMonth(Month month)
    {
        if (Months.Any(m => m.Month.Equals(month)))
            throw new InvalidOperationException($"Month {month} already exists in the budget year {Year}.");

        var budgetMonth = new BudgetMonth(month, BudgetId);
        Months.Add(budgetMonth);
    }
    
    public void RemoveMonth(Month month)
    {
        var budgetMonth = Months.FirstOrDefault(m => m.Month.Equals(month));
        if (budgetMonth == null)
            throw new InvalidOperationException($"Month {month} does not exist in the budget year {Year} - {BudgetId}.");

        Months.Remove(budgetMonth);
    }
    
    public BudgetMonth? GetMonth(Month month)
        => Months.FirstOrDefault(m => m.Month.Equals(month));
    
    
    public IEnumerable<BudgetMonth> GetAllMonths()
        => Months.AsReadOnly();
}
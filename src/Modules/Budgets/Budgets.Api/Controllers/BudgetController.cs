using Budgets.Core.Commands.BudgetManagement.Dtos;
using Budgets.Core.Commands.BudgetManagement.Services;
using Budgets.Core.Queries.Budget;
using Budgets.Core.Queries.Budget.Dtos;
using Budgets.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Budgets.Api.Controllers;

public class BudgetController : BudgetBaseController
{
    private readonly IBudgetManagement _budgetManagementService;
    private readonly IBudgetQueryService _budgetQueryService;

    public BudgetController(IBudgetManagement budgetManagementService, IBudgetQueryService budgetQueryService)
    {
        _budgetManagementService = budgetManagementService;
        _budgetQueryService = budgetQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBudgets(CancellationToken cancellationToken)
    {
        var budgets = await _budgetQueryService.GetAllAsync(cancellationToken);
        return Ok(budgets);
    }
    
    [HttpGet("{budgetId:guid}")]
    public async Task<IActionResult> GetBudgetById(Guid budgetId, CancellationToken cancellationToken)
    {
        var budgetDetails = await _budgetQueryService.GetByIdAsync(budgetId, cancellationToken);
        return Ok(budgetDetails);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetDto createBudgetDto,
        CancellationToken cancellationToken)
    {
        var budgetId = await _budgetManagementService.CreateAsync(createBudgetDto, cancellationToken);
        return Ok(new { Id = budgetId.Value });
    }

    [HttpPut]
    [Route("{budgetId:guid}")]
    public async Task<IActionResult> UpdateBudgetDetails(Guid budgetId, [FromBody] UpdateBudgetDetailsDto detailsDto,
        CancellationToken cancellationToken)
    {
        var isValid = new BudgetId(detailsDto.Id).Equals(new BudgetId(budgetId));
        if (!isValid)
        {
            return BadRequest("Budget ID in the URL does not match the ID in the request body.");
        }

        await _budgetManagementService.UpdateDetailsAsync(detailsDto, cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    [Route("{budgetId:guid}")]
    public async Task<IActionResult> DeleteBudget(Guid budgetId, CancellationToken cancellationToken)
    {
        await _budgetManagementService.DeleteAsync(budgetId, cancellationToken);
        return NoContent();
    }
}
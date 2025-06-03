using Budgets.Core.Commands.Categories.Dtos;
using Budgets.Core.Commands.Categories.Services;
using Budgets.Core.Queries.Categories;
using Budgets.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Budgets.Api.Controllers;

[Route(RouteBase + "/{id}/categories")]
public class CategoriesController : BudgetBaseController
{
    private readonly ICategoryService _categoryService;
    private readonly ICategoryQueryService _categoryQueryService;

    public CategoriesController(ICategoryService categoryService, ICategoryQueryService categoryQueryService )
    {
        _categoryService = categoryService;
        _categoryQueryService = categoryQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories(Guid id, CancellationToken cancellationToken)
    {
        var categories = await _categoryQueryService.GetByBudgetIdAsync(id, cancellationToken);
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
    {
        var categoryId = await _categoryService.CreateAsync(createCategoryDto, cancellationToken);
        return Ok(new { Id = categoryId.Value });
    }

    [HttpPut]
    [Route("{budgetId:guid}")]
    public async Task<IActionResult> UpdateCategory(Guid budgetId, [FromBody] UpdateCategoryDto categoryDto,
        CancellationToken cancellationToken)
    {
        var isValid = new BudgetId(categoryDto.BudgetId).Equals(new BudgetId(budgetId));
        if (!isValid)
        {
            return BadRequest("Budget ID in the URL does not match the ID in the request body.");
        }

        await _categoryService.UpdateAsync(categoryDto, cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    [Route("{budgetId:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid budgetId, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteAsync(budgetId, cancellationToken);
        return NoContent();
    }
}
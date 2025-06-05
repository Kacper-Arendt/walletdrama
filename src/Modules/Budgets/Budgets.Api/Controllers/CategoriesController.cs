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
    [Route("{categoryId:guid}")]
    public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] UpdateCategoryDto categoryDto,
        CancellationToken cancellationToken)
    {
        var isValid = new CategoryId(categoryDto.Id).Equals(new CategoryId(categoryId));
        if (!isValid)
        {
            return BadRequest("CategoryId in the URL does not match the ID in the request body.");
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
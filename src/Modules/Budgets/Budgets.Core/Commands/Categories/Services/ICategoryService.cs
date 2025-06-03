using Budgets.Core.Commands.Categories.Dtos;
using Budgets.Domain.ValueObjects;

namespace Budgets.Core.Commands.Categories.Services;

public interface ICategoryService
{
    Task<CategoryId> CreateAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken);
}
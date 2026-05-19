using StockIt.Application.DTOs.Category;
using StockIt.Domain.Shared;

namespace StockIt.Application.UseCases.Category.Register;

public interface IRegisterCategoryUseCase
{
    public Task<Result<CategoryResponse>> Execute(CategoryRequest request);
}

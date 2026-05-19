using StockIt.Application.DTOs.Category;
using StockIt.Application.MappingProfiles;
using StockIt.Application.Services;
using StockIt.Domain.Repositories;
using StockIt.Domain.Shared;
using StockIt.Domain.Shared.Errors;

namespace StockIt.Application.UseCases.Category.Register;

public class RegisterCategoryUseCase(IUnitOfWork unitOfWork, ILoggedUser loggedUser) : IRegisterCategoryUseCase
{
    public async Task<Result<CategoryResponse>> Execute(CategoryRequest request)
    {
        var validationResult = await Validate(request);

        if (!validationResult.IsSuccess) return Result<CategoryResponse>.Failure(validationResult.Error!);

        var category = request.ToCategoryEntity();
        category.CompanyId = loggedUser.GetCompanyId();

        var user = await unitOfWork.Users.GetUserByEmail(loggedUser.GetUserEmail());
        category.UserId = user!.Id;

        await unitOfWork.Categories.Add(category);

        await unitOfWork.SaveAsync();

        var response = category.ToCategoryResponse();

        return Result<CategoryResponse>.Success(response);
    }

    private static async Task<Result> Validate(CategoryRequest request)
    {
        var validationResult = new RegisterCategoryValidator().Validate(request);

        if(!validationResult.IsValid)
        {
            var errorsMessages = validationResult.Errors.Select(e => e.ErrorMessage);

            return Result.Failure(new ValidationError(errorsMessages));
        }

        return Result.Success();
    }
}

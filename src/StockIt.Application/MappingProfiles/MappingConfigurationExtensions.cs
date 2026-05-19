using StockIt.Application.DTOs.Category;
using StockIt.Application.DTOs.User;
using StockIt.Domain.Entities;

namespace StockIt.Application.MappingProfiles;

public static class MappingConfigurationExtensions
{
    // Request to Entity
    public static User ToUserEntity(this RegisterUserRequest request) => new() { Name = request.Name,  Email = request.Email };

    public static Company ToCompanyEntity(this RegisterUserRequest request) => new() { Name = request.CompanyName!};

    public static Category ToCategoryEntity(this CategoryRequest request) => new() { Name = request.Name };

    // Entity to Response
    public static RegisteredUserResponse ToRegisteredUserResponse(this User user) => new(user.Name, user.Email, user.CompanyId, user.Role);

    public static CategoryResponse ToCategoryResponse(this Category category) => new(category.Name);
}

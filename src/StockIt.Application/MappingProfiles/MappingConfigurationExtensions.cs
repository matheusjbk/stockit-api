using StockIt.Application.DTOs.User;

namespace StockIt.Application.MappingProfiles;

public static class MappingConfigurationExtensions
{
    public static RegisteredUserResponse ToRegisteredUserResponse(this RegisterUserRequest request) => new(request.Name, request.Email);
}

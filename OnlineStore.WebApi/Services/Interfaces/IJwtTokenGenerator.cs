using OnlineStore.WebApi.Models;

namespace OnlineStore.WebApi.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateJwtTokenString(AppUser user);
    }
}

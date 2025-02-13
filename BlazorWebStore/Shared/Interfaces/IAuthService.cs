using Shared.DTOs;

namespace Shared.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(UserLoginDTO request);
    }
}
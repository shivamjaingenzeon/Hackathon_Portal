using hackathonbackend.Models;
using Microsoft.AspNetCore.Identity;

namespace hackathonbackend.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(TeamSignInDto singInDto);
        Task<ResponseDto<T>> LoginAsync<T>(LoginDto loginDto) where T : class;
    }
}

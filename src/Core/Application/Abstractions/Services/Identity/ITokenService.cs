using CleanApi.Application.Wrapper;
using CleanApi.Shared.DTOs.Identity.Requests;
using CleanApi.Shared.DTOs.Identity.Responses;
using System.Threading.Tasks;

namespace CleanApi.Application.Abstractions.Services.Identity
{
    public interface ITokenService : ITransientService
    {
        Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
    }
}
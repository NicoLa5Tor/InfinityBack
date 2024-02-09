using BackInfinity.Models.Custom;

namespace BackInfinity.Services.Contract
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResponse> ReturnToken(AuthorizationRequest aut);
    }
}

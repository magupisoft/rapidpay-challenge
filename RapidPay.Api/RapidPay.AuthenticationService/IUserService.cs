using RapidPay.Domain.Responses;
using RapidPay.Storage.DbModel;
using System.Threading.Tasks;

namespace RapidPay.AuthenticationService
{
    public interface IUserService
    {
        Task<(AuthenticatedUserResponse authenticatedUser, User user)> AuthenticateAsync(string username, string password);
    }
}

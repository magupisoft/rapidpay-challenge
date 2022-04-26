using RapidPay.Domain.Responses;
using RapidPay.Storage.DbModel;
using System.Threading.Tasks;

namespace RapidPay.AuthenticationService
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}

using RapidPay.Storage.DbModel;
using System.Threading.Tasks;

namespace RapidPay.Storage.Repository
{
    public interface IUserRepository
    {
        public Task<User> AuthenticateAsync(string username, string password);
    }
}

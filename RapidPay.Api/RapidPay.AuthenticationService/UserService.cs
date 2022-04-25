using RapidPay.Domain.Responses;
using RapidPay.Storage.DbModel;
using RapidPay.Storage.Repository;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.AuthenticationService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<(AuthenticatedUserResponse authenticatedUser, User user)> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.AuthenticateAsync(username, password);
            AuthenticatedUserResponse response = null;
            if (user != null)
            {
                user.Password = null;
                var credentials = $"{username}:{password}";
                byte[] credentialsAsBytes = Encoding.UTF8.GetBytes(credentials);
                response = new AuthenticatedUserResponse
                {
                    AuthorizarionHeader = Convert.ToBase64String(credentialsAsBytes)
                };
            }
            return (response, user);
        }
    }
}

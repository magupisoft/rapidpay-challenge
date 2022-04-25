using Microsoft.EntityFrameworkCore;
using RapidPay.Storage.DbModel;
using System.Threading.Tasks;

namespace RapidPay.Storage.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private new readonly RapidPayContext _context;

        public UserRepository(RapidPayContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                return null;
            }

            user.Password = null;
            return user;
        }
    }
}

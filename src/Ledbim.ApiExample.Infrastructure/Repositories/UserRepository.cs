using Ledbim.ApiExample.Domain.Entities;
using Ledbim.ApiExample.Domain.Repositories;
using Ledbim.Core.Base.Repositories;

namespace Ledbim.ApiExample.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) { }
    }
}
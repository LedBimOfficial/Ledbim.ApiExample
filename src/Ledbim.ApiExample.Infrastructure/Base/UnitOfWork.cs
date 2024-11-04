using Ledbim.ApiExample.Domain.Base;
using Ledbim.ApiExample.Domain.Repositories;
using Ledbim.ApiExample.Infrastructure.Repositories;

namespace Ledbim.ApiExample.Infrastructure.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUserRepository _users = null!;

        public UnitOfWork(ApplicationContext context)
        {
            _users = new UserRepository(context);
        }

        public IUserRepository Users => _users;
    }
}

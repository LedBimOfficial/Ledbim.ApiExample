using Ledbim.ApiExample.Domain.Repositories;

namespace Ledbim.ApiExample.Domain.Base
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
    }
}

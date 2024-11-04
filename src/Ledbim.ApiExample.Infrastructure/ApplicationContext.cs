using Ledbim.ApiExample.Domain.Entities;
using Ledbim.Core.Base.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Ledbim.ApiExample.Infrastructure
{
    public class ApplicationContext : BaseContext
    {
        public DbSet<User> Users { get; set; }


        public ApplicationContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options, httpContextAccessor) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

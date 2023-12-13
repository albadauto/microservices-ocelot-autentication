using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TesteMicroServiços.Context
{
    public class SqlServerContext : IdentityDbContext
    {
        public SqlServerContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}

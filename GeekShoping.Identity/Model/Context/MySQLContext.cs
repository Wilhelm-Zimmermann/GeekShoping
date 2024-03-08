using GeekShoping.Identity.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShoping.Identity.Model.Context
{
    public class MySQLContext : IdentityDbContext<ApplicationUser>
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {

        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace taskplanner.Models
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ScheduledTask> ScheduledTasks { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

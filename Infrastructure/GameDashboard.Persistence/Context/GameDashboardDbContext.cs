using GameDashboard.Domain.Concrete.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Persistence.Context
{
    public class GameDashboardDbContext:IdentityDbContext<AppUser,AppRole,Guid>
    {
        public GameDashboardDbContext(DbContextOptions<GameDashboardDbContext> options):base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppRole>().HasData(new { Id = new Guid("2355ccfb-5e9e-4a8c-972d-384fb2315457"), Name = "User", NormalizedName = "USER" });
            base.OnModelCreating(builder);
        }
    }   
}

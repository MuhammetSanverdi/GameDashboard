using GameDashboard.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace GameDashboard.Domain.Concrete.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public List<AppUser> AppUsers { get; set; }
    }
}

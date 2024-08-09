using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Domain.Concrete.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public List<AppRole> AppRoles { get; set; }
    }
}

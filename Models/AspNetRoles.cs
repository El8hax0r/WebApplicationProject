using System;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaims>();
            // scaffolding got rid of "AspNetUserRoles"
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; }
    }
}

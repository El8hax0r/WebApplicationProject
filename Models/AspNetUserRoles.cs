using System;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetUsers User { get; set; }
        //auto scaffolding got rid of "Role"
    }
}

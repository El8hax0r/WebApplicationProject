using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace WebApplication1
{
    [Authorize]
    public partial class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }

        public virtual UserClass UserClass { get; set; }
    }
}

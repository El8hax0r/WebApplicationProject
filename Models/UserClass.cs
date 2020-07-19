using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1
{
    public partial class UserClass
    {
        public int ClassId { get; set; }
        public string Id { get; set; }


        public virtual Class Class { get; set; }
        public virtual AspNetUsers IdNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class UserClass
    {
        public int UserClassId { get; set; }
        public int ClassId { get; set; }
        public string Id { get; set; }

        public virtual Class Class { get; set; }
        public virtual AspNetUsers IdNavigation { get; set; }
    }
}

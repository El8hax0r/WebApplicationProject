using System;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }

        public virtual UserClass UserClass { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class OTPValidator
    {
        public virtual string Email { get; set; }
        public virtual long CurrentOtp { get; set; }
        public virtual DateTime DateTime { get; set; }
    }

}
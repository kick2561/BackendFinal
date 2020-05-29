using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class StoreInfo
    {
        public virtual long StoreId { get; set; }
        public virtual string StoreName { get; set; }
        public virtual string Phone{ get; set; }
        public virtual Location StoreLocation { set; get; }
        public virtual long SlotDays { set; get; }
        public virtual DateTime SlotStartTime { set; get; }
        public virtual DateTime SlotEndTime { set; get; }
        public virtual long SlotDuration { set; get; }
        public virtual string Email { set; get; }
    }
}
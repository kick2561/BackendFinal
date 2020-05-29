using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Models
{
    public class OrderModel
    {
        public virtual long OrderId { get; set; }
        public virtual string SlotTime { set; get; }
        public virtual string SlotDate
        { set; get; }
        public virtual string StoreName { set; get; }
        public virtual string Phone { get; set; }
        public List<ItemCatalogueModel> ListItems { set; get; }
        public virtual string StoreAddress { get; set; }
    }
}
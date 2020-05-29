using StoreBuy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Models
{
    public class StoreModel
    {
        public virtual long StoreId { get; set; }
        public virtual string StoreName { get; set; }
        public virtual string Phone { get; set; }
        public List<ItemCatalogueModel> ListItems { set; get; }
        public virtual string StoreAddress { get; set; }
        public virtual long SlotDays { set; get; }
        public virtual DateTime SlotStartTime { set; get; }
        public virtual DateTime SlotEndTime { set; get; }
        public virtual long SlotDuration { set; get; }
        public virtual long Latitude { get; set; }
        public virtual long Longitude { get; set; }

    }
}
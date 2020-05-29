using StoreBuy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Interfaces
{
    public interface IStoreFilledSlotRepository : IGenericRepository<StoreFilledSlot>
    {
        IEnumerable<StoreFilledSlot> RetrieveFilledSlotsByDate(StoreInfo Store, string Date);
    }
}
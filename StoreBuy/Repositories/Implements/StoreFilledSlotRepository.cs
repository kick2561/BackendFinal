using NHibernate;
using StoreBuy.Domain;
using StoreBuy.Repositories.Interfaces;
using StoreBuy.UnitofWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Implements
{
    public class StoreFilledSlotRepository : GenericRepository<StoreFilledSlot>,IStoreFilledSlotRepository
    {
        public StoreFilledSlotRepository(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public IEnumerable<StoreFilledSlot> RetrieveFilledSlotsByDate(StoreInfo Store, string Date)
        {
            try
            {
                var Slots = Session.Query<StoreFilledSlot>().ToList().Where(x => x.Store == Store && x.SlotDateTime.ToString("dd-MMMM-yyyy") == Date);
                return Slots;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
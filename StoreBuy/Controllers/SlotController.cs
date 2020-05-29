using StoreBuy.Domain;
using StoreBuy.Repositories;
using StoreBuy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreBuy.Controllers
{
    [RoutePrefix("api/Slot")]
    public class SlotController : ApiController
    {

        IStoreFilledSlotRepository SlotRepository = null;
        IStoreSearchRepository StoreRepository=null;
        IOrdersRepository OrdersRepository = null;
        IUserRepository UserRepository = null;
        
        public SlotController(IStoreFilledSlotRepository SlotRepository,
            IStoreSearchRepository StoreRepository, IOrdersRepository OrdersRepository, IUserRepository UserRepository)
        {
            this.StoreRepository = StoreRepository;
            this.SlotRepository = SlotRepository;
            this.OrdersRepository = OrdersRepository;
            this.UserRepository = UserRepository;
        }

        [HttpGet]
        [Route("GetFilledSlots")]
        public List<string> GetFilledSlots(long StoreId, string Date,long UserId)
        {
            DeletePreviousSlots();
            StoreInfo Store = StoreRepository.GetById(StoreId);
            var Slots = SlotRepository.RetrieveFilledSlotsByDate(Store, Date).ToList();
            List<string> FilledSlotTimes = new List<string>();
            foreach (StoreFilledSlot Slot in Slots)
            {
                var SlotTime = Slot.SlotDateTime.ToString("HH:mm");
                FilledSlotTimes.Add(SlotTime);
            }
            Users User = UserRepository.GetById(UserId);
            List<Orders> Orders = OrdersRepository.RetrieveOrdersByUser(User).Where(x => x.SlotDate == Date).ToList();
            foreach (Orders order in Orders)
            {
                var SlotTime = order.SlotTime.Substring(0, 5);
                FilledSlotTimes.Add(SlotTime);
            }
            return FilledSlotTimes;
        }



        private void DeletePreviousSlots()
        {
            var AllSlots = SlotRepository.GetAll();
            foreach (StoreFilledSlot Slot in AllSlots)
            {

                if (Slot.SlotDateTime < DateTime.Now)
                {
                    SlotRepository.Delete(Slot.SlotId);
                }
            }
        }

    }
}

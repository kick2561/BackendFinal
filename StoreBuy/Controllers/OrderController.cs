using StoreBuy.Domain;
using StoreBuy.Models;
using StoreBuy.Repositories;
using StoreBuy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Mail;
using System.Web.Http;

namespace StoreBuy.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {

        IOrdersRepository OrdersRepository = null;
        IStoreSearchRepository StoreRepository = null;
        IUserRepository UserRepository = null;
        IOrderItemRepository OrderItemRepository = null;
        IItemRepository ItemRepository = null;
        ICartRepository CartRepository = null;
        IStoreFilledSlotRepository StoreFilledRepository = null;

        public OrderController(IOrdersRepository OrdersRepository,
            IStoreSearchRepository StoreRepository,
            IUserRepository UserRepository,
            IOrderItemRepository OrderItemRepository,
            IItemRepository ItemRepository,
            IStoreFilledSlotRepository StoreFilledRepository,
            ICartRepository CartRepository)
        {
            this.CartRepository = CartRepository;
            this.StoreFilledRepository = StoreFilledRepository;
            this.ItemRepository = ItemRepository;
            this.OrderItemRepository = OrderItemRepository;
            this.UserRepository = UserRepository;
            this.OrdersRepository = OrdersRepository;
            this.StoreRepository = StoreRepository;
        }

        [HttpPost]
        [Route("InsertOrder")]
        public HttpResponseMessage InsertOrder(FormDataCollection Collection)
        {
            try
            {
                var UserId = Int64.Parse(Collection["UserId"]);
                var StoreId = Int64.Parse(Collection["StoreId"]);
                var SlotTime = Collection["SlotTime"];
                var SlotDate = Collection["SlotDate"];
                Users User = UserRepository.GetById(UserId);
                StoreInfo StoreInfo = StoreRepository.GetById(StoreId);
                Orders Order = new Orders();
                Order.SlotDate = SlotDate;
                Order.SlotTime = SlotTime;
                Order.User = User;
                Order.Store = StoreInfo;
                FillSlot(Order, SlotDate, SlotTime, StoreInfo);
                OrdersRepository.InsertOrUpdate(Order);
                List<ItemCatalogueModel> ItemList = StoreRepository.ItemsAvailableInStore(UserId, StoreInfo);

                foreach (ItemCatalogueModel item in ItemList)
                {
                    var Item = ItemRepository.GetById(item.ItemId);
                    OrderItem OrderItem = new OrderItem();
                    OrderItem.Order = Order;
                    OrderItem.Item = Item;
                    OrderItem.Quantity = item.Quantity;
                    OrderItemRepository.InsertOrUpdate(OrderItem);

                }
                OrdersRepository.NotifyUser(Order, ItemList, User.Email);
                OrdersRepository.NotifyRetailer(Order, ItemList, StoreInfo.Email);

                DeleteCartItems(ItemList, User);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        void DeleteCartItems(List<ItemCatalogueModel> ItemList, Users User)
        {
            foreach (ItemCatalogueModel item in ItemList)
            {
                var DeleteItem = ItemRepository.GetById(item.ItemId);
                CartRepository.DeleteCartItemOfUser(User, DeleteItem);
            }
        }

        void FillSlot(Orders Order, string SlotDate, string SlotTime, StoreInfo Store)
        {
            int SlotCount = OrdersRepository.GetSlotCount(Order);
            if (SlotCount == Int32.Parse(Resources.LimitPerSlot))
            {
                StoreFilledSlot StoreFilledSlot = new StoreFilledSlot();
                var DateAndTime = SlotDate + " " + SlotTime.Substring(0, 5);
                DateTime SlotDateTime = DateTime.ParseExact(DateAndTime, Resources.DateTimeFormat,
                                       System.Globalization.CultureInfo.InvariantCulture);
                StoreFilledSlot.SlotDateTime = SlotDateTime;
                StoreFilledSlot.Store = Store;
                StoreFilledRepository.InsertOrUpdate(StoreFilledSlot);
            }
        }

        [HttpGet]
        [Route("GetOrdersByUser")]
        public IEnumerable<OrderModel> GetOrdersByUser(long UserId)
        {
            Users User = UserRepository.GetById(UserId);
            List<Orders> Orders =  OrdersRepository.RetrieveOrdersByUser(User);
            List<OrderModel> UserOrders = new List<OrderModel>();
            foreach (Orders Order in Orders)
            {
                OrderModel UserOrder = new OrderModel();
                UserOrder.OrderId = Order.OrderId;
                UserOrder.StoreName = Order.Store.StoreName;
                UserOrder.StoreAddress = Order.Store.StoreLocation.Address;
                UserOrder.SlotDate = Order.SlotDate;
                UserOrder.SlotTime = Order.SlotTime;
                UserOrder.Phone = Order.Store.Phone;
                var OrderItems = OrderItemRepository.RetrieveOrderItems(Order);
                List<ItemCatalogueModel> Items = new List<ItemCatalogueModel>();
                foreach (OrderItem OrderItem in OrderItems)
                {
                    ItemCatalogueModel Item = new ItemCatalogueModel();
                    Item.ItemId = OrderItem.Item.ItemId;
                    Item.ItemName = OrderItem.Item.ItemName;
                    Item.Quantity = OrderItem.Quantity;
                    Item.Price = StoreRepository.GetPriceIfItemAvailableInStore(OrderItem.Item.ItemId,Order.Store);
                    Items.Add(Item);
                }
                UserOrder.ListItems = Items;
                UserOrders.Add(UserOrder);
            }
            return UserOrders;
        }
    }
}

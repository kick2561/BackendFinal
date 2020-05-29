using StoreBuy.Domain;
using StoreBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Interfaces
{
    public interface IOrdersRepository : IGenericRepository<Orders>
    {
        bool NotifyUser(Orders order, List<ItemCatalogueModel> Items,string Email);
        bool NotifyRetailer(Orders order, List<ItemCatalogueModel> Items, string Email);
        List<Orders> RetrieveOrdersByUser(Users User);
        int GetSlotCount(Orders order);
    }
}
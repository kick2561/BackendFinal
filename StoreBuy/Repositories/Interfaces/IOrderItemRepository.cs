﻿using StoreBuy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Interfaces
{
    public interface IOrderItemRepository:IGenericRepository<OrderItem>
    {
        List<OrderItem> RetrieveOrderItems(Orders Order);
    }
}
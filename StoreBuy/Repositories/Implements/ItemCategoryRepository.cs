using StoreBuy.Domain;
using StoreBuy.Repositories.Interfaces;
using StoreBuy.UnitofWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Implements
{
    public class ItemCategoryRepository:GenericRepository<ItemCategory>,IItemCategoryRepository
    {
        public ItemCategoryRepository(IUnitOfWork UnitOfWork):base(UnitOfWork)
        {

        }
    }
}
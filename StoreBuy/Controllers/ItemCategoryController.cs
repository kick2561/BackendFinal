using StoreBuy.Domain;
using StoreBuy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace StoreBuy.Controllers
{
    [RoutePrefix("api/itemcategory")]
    public class ItemCategoryController : ApiController
    {
        IItemCategoryRepository ItemCategoryRepository = null;



        public ItemCategoryController(IItemCategoryRepository ItemCategoryRepository)
        {
            this.ItemCategoryRepository = ItemCategoryRepository;



        }



        [HttpGet]
        [Route("GetAllItemCategories")]
        public IEnumerable<ItemCategory> GetAllItemCategories()
        {
            try
            {
                return ItemCategoryRepository.GetAll();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
}

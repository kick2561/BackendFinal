using StoreBuy.Repositories;
using StoreBuy.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoreBuy.Models;
using StoreBuy.Repositories.Interfaces;
using System.Net.Http.Formatting;

namespace StoreBuy.Controllers
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        ICartRepository CartRepository = null;
        IUserRepository UserRepository = null;
        IItemRepository ItemRepository = null;
        
        public CartController(ICartRepository CartRepository,
            IUserRepository UserRepository,
            IItemRepository ItemRepository)
        {
            this.CartRepository = CartRepository;
            this.UserRepository = UserRepository;
            this.ItemRepository = ItemRepository;            
        }

        [HttpGet]
        [Route("CartItems")]
        public IEnumerable<ItemCatalogueModel> GetCartItems(long UserId)
        {
            List<ItemCatalogueModel> CartList = new List<ItemCatalogueModel>();
            Users User = UserRepository.GetById(UserId);
            var CartItems = CartRepository.RetrieveCartItemsOfUser(User);



            foreach (Cart Cart in CartItems)
            {
                ItemCatalogueModel CartItem = new ItemCatalogueModel();
                CartItem.ItemId = Cart.ItemCatalogue.ItemId;
                CartItem.ItemName = Cart.ItemCatalogue.ItemName;
                CartItem.ItemImage = Cart.ItemCatalogue.ItemImage;
                CartItem.Price = Cart.ItemCatalogue.EstimatedPrice;
                CartItem.ItemDescription = Cart.ItemCatalogue.ItemDescription;
                CartItem.CategoryName = Cart.ItemCatalogue.ItemCategory.CategoryName;
                CartItem.Quantity = Cart.Quantity;
                CartList.Add(CartItem);
            }
            return CartList;
        }

        [HttpPost]
        [Route("AddToCart")]
        public HttpResponseMessage AddToCart(FormDataCollection Collection)
        {
            try
            {

                long UserId =Int64.Parse( Collection["UserId"]);
                long ItemId = Int64.Parse(Collection["ItemId"]);
                long Quantity = Int64.Parse(Collection["Quantity"]);
                Users User = UserRepository.GetById(UserId);
                ItemCatalogue ItemCatalogue = ItemRepository.GetById(ItemId);
                Cart Cart = CartRepository.RetrieveExistingCartItem(User, ItemCatalogue);

                if (Cart == null)
                {
                    Cart = new Cart();
                    Cart.ItemCatalogue = ItemCatalogue;
                    Cart.User = User;
                    Cart.Quantity = Quantity;

                    CartRepository.InsertOrUpdate(Cart);
                    return Request.CreateResponse(HttpStatusCode.OK, "Successfully added");
                }
                else
                {
                    Cart.Quantity = Quantity;

                    CartRepository.InsertOrUpdate(Cart);
                    return Request.CreateResponse(HttpStatusCode.Found, "Successfully added quantity");
                }

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, "unable to add to cart");
            }
        }


        [HttpPost]
        [Route("UpdateQuantity")]
        public HttpResponseMessage UpdateQuantity(FormDataCollection Collection)
        {
            try
            {
                long UserId = Int64.Parse(Collection["UserId"]);
                long ItemId = Int64.Parse(Collection["ItemId"]);
                int Quantity = Int32.Parse(Collection["Quantity"]);
                Users User = UserRepository.GetById(UserId);
                ItemCatalogue ItemCatalogue = ItemRepository.GetById(ItemId);
                Cart Cart = CartRepository.RetrieveExistingCartItem(User, ItemCatalogue);
                Cart.Quantity = Quantity;
                CartRepository.InsertOrUpdate(Cart);
                return Request.CreateResponse(HttpStatusCode.OK, "Updated Successfully");



            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Update unsuccessfull");
            }
        }




        [HttpPost]
        [Route("DeleteCartItem")]
        public HttpResponseMessage DeleteCartItem(FormDataCollection Collection)
        {
            try
            {
                long UserId = Int64.Parse(Collection["UserId"]);
                long ItemId = Int64.Parse(Collection["ItemId"]);
                Users User = UserRepository.GetById(UserId);
                ItemCatalogue ItemCatalogue = ItemRepository.GetById(ItemId);
                CartRepository.DeleteCartItemOfUser(User, ItemCatalogue);
                return Request.CreateResponse(HttpStatusCode.OK, "Deleted Successfully");

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Delete unsuccessfull");
            }
        }

    }
}
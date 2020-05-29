using Newtonsoft.Json;
using StoreBuy.Domain;
using StoreBuy.Models;
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
    [RoutePrefix("api/StoreSearch")]
    public class StoreSearchController : ApiController
    {

        ICartRepository CartRepository = null;
        IStoreSearchRepository StoreRepository = null;

       public  StoreSearchController(ICartRepository CartRepository,
           IStoreSearchRepository StoreRepository)
        {
            this.CartRepository = CartRepository;
            this.StoreRepository = StoreRepository;            
        }

        [HttpGet]
        [Route("FindStoresByItems")]
        public List<StoreModel> FindStoresByItems(long UserId, long Latitude,long Longitude)
        {
            List<StoreInfo> AllListOfStores = StoreRepository.GetAll().ToList();
            List<StoreInfo> ListOfStores =  StoreRepository.FindNearestStores(Latitude,Longitude,AllListOfStores);
            Dictionary<StoreInfo, List<ItemCatalogueModel>> StoresToAvailableItemsMap = new Dictionary<StoreInfo, List<ItemCatalogueModel>>();

            foreach (StoreInfo Store in ListOfStores)
            {
                List<ItemCatalogueModel> ItemsList = StoreRepository.ItemsAvailableInStore(UserId, Store);
                StoresToAvailableItemsMap.Add(Store, ItemsList);
            }

            List<StoreModel> Stores = new List<StoreModel>();

            foreach (KeyValuePair<StoreInfo, List<ItemCatalogueModel>> Item in StoresToAvailableItemsMap.OrderBy(key => -key.Value.Count))
            {
                var Storemodel = new StoreModel();
                Storemodel.StoreId = Item.Key.StoreId;
                Storemodel.StoreName = Item.Key.StoreName;
                Storemodel.Phone = Item.Key.Phone;
                Storemodel.ListItems = Item.Value;
                Storemodel.StoreAddress = Item.Key.StoreLocation.Address;
                Storemodel.SlotDays = Item.Key.SlotDays;
                Storemodel.SlotStartTime = Item.Key.SlotStartTime;
                Storemodel.SlotEndTime = Item.Key.SlotEndTime;
                Storemodel.SlotDuration = Item.Key.SlotDuration;
                Storemodel.Latitude = Item.Key.StoreLocation.Latitude;
                Storemodel.Longitude = Item.Key.StoreLocation.Longitude;
                Stores.Add(Storemodel);
            }
            return Stores;
        }

        [HttpGet]
        [Route("FindStoresByDistance")]
        public List<StoreModel> FindStoresByDistance(long UserId, long Latitude, long Longitude)
        {
            List<StoreInfo> AllListOfStores = StoreRepository.GetAll().ToList();
            List<StoreInfo> ListOfStores = StoreRepository.FindNearestStores(Latitude, Longitude, AllListOfStores);
            List<StoreModel> Stores = new List<StoreModel>();



            foreach (StoreInfo Store in ListOfStores)
            {
                List<ItemCatalogueModel> ItemsList = StoreRepository.ItemsAvailableInStore(UserId, Store);
                var Storemodel = new StoreModel();
                Storemodel.StoreId = Store.StoreId;
                Storemodel.StoreName = Store.StoreName;
                Storemodel.Phone = Store.Phone;
                Storemodel.ListItems = ItemsList;
                Storemodel.StoreAddress = Store.StoreLocation.Address;
                Storemodel.SlotDays = Store.SlotDays;
                Storemodel.SlotStartTime = Store.SlotStartTime;
                Storemodel.SlotEndTime = Store.SlotEndTime;
                Storemodel.SlotDuration = Store.SlotDuration;
                Storemodel.Longitude = Store.StoreLocation.Longitude;
                Storemodel.Latitude = Store.StoreLocation.Latitude;
                Stores.Add(Storemodel);
            }

            return Stores;
        }

    }
}

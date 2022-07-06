using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopModel;
using ShopServiceCommon.Common;
using ShopGameRepository.Common;
using ShopRepositoryRepository;
using GameShop.Common;

namespace ShopGame.Service
{
    public class ShopService : IShopService
    {
        private IShopRepository _repository;

        public ShopService(IShopRepository objShopRepo)
        {
            this._repository = objShopRepo;
        }
        public Shop AddNewShop(Shop value)
        {
            return _repository.AddNewShop(value);
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<Shop>> GetAllShopsAsync(Pagination pagination, Filter filter, Sort sort)
        {
            return await _repository.GetAllShopsAsync(pagination, filter, sort);
        }

        public async Task<Shop> GetOneShopAsync(Guid id)
        {
            return await _repository.GetOneShopAsync(id);
        }

        public Shop Put(Guid id, Shop value)
        {
            return _repository.Put(id, value);
        }

    }
}

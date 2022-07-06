using ShopModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Common;

namespace ShopGameRepository.Common
{
    public interface IShopRepository
    {
        Shop AddNewShop(Shop value);
        Task<string> DeleteAsync(Guid id);
        Task<List<Shop>> GetAllShopsAsync(Pagination pagination, Filter filter, Sort sort);
        Task<Shop> GetOneShopAsync(Guid id);
        Shop Put(Guid id, Shop value);
    }
}
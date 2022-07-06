using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Common;
using ShopModel;

namespace ShopServiceCommon.Common
{
    public interface IShopService
    {
        Shop AddNewShop(Shop value);
        Task<string> DeleteAsync(Guid id);
        Task<List<Shop>> GetAllShopsAsync(Pagination pagination, Filter filter, Sort sort);
        Task<Shop> GetOneShopAsync(Guid id);
        Shop Put(Guid id, Shop value);
    }
}

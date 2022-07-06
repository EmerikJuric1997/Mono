using Autofac;
using ShopModel;
using ShopGame.Service;
using ShopServiceCommon.Common;
using System.Reflection;
using System.Web.Http;
using GameShop.Model.Common;
using Autofac.Integration.WebApi;
using ShopRepositoryRepository;
using ShopGameRepository.Common;

namespace project.WebApi
{
    public class DependencyInjectionBuilder
    {
        public void Build()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ShopService>().As<IShopService>();
            builder.RegisterType<ShopRepository>().As<IShopRepository>();
            builder.RegisterType<Shop>().As<IShop>();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
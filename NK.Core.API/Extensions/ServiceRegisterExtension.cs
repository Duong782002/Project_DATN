using NK.Core.Business.Service;
using NK.Core.Business.Service.Implement;
using NK.Core.Business.Service.Interface;
using NK.Core.DataAccess.Repository;

namespace SH.Core.API.Extensions
{
    public static class ServiceRegisterExtension
    {
        public static void ServiceRegister(this IServiceCollection services)
        {
            services.AddSingleton<IGlobalServices, GlobalServices>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IWishListsService, WishListsService>();
            services.AddScoped<ISoleService, SoleService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IBrandService, BrandService>();  
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IWardsService, WardsService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IWishListsRepository, WishListsRepository>();
            services.AddScoped<ISoleRepository, SoleRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
        } 
    }
}

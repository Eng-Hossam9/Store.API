using Demo.Core.RepositoriesInterFaces;
using Demo.Core.ServicesInterFaces;
using Demo.Repository.Data.Contexts;
using Demo.Repository.Repositories;
using Demo.Service.Services.Brand;
using Demo.Service.Services.Type;
using Demo.Service.Services;
using Microsoft.EntityFrameworkCore;
using Demo.Core.Mapping;
using Microsoft.AspNetCore.Mvc;
using Demo.API.Errors;
using Demo.Repository.Repositories.Baskets;
using StackExchange.Redis;
using Demo.Service.Services.Caches;
using Demo.Repository.Identity.Context;
using Demo.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Demo.Service.Services.Token;
using Demo.Service.Services.Accounts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Demo.Core.Mapping.Account;

namespace Demo.API.ProgramConfiguration

{
    public static class DependancyInjection
    {
        public static IServiceCollection AddDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            services.BuiltInServices();
            services.SwagerServices();
            services.DbContextsService(configuration);
            services.UserDefiendService();
            services.MapperService(configuration);
            services.InvalidModelstateResponseService(configuration);
           services.RedisService(configuration);
            services.IdentityServices();
            services.AuthenticationServices(configuration);
            return services;
        }

        private static IServiceCollection BuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection SwagerServices(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
        private static IServiceCollection DbContextsService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<StoreDbContext>(option =>
                               option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<StoreIdentityDbContext>(option =>
                              option.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));



            return services;
        }

        private static IServiceCollection UserDefiendService(this IServiceCollection services)
        {

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandSevices>();
            services.AddScoped<ITypeService, TypeServices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped<IcacheService,CacheService>();
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IUserService,UserService>();


            return services;
        }

        private static IServiceCollection MapperService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new CustomerBasketProfile()));
            services.AddAutoMapper(m => m.AddProfile(new AddressProfile()));

            return services;
        }


        private static IServiceCollection InvalidModelstateResponseService(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count() > 0)
                                                        .SelectMany(e => e.Value.Errors)
                                                        .Select(e => e.ErrorMessage).ToList();

                    var respons = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(respons);
                };
            });
            return services;
        }





        private static IServiceCollection RedisService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
              var connection=  configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });
         

            return services;
        }



        private static IServiceCollection IdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();


            return services;
        }


        private static IServiceCollection AuthenticationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer=true,
                    ValidIssuer= configuration["Jwt:Issuer"],
                    ValidateAudience=true,
                    ValidAudience= configuration["Jwt:Audience"],
                    ValidateLifetime=true,
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]))

                };
            });



            return services;
        }



    }
}

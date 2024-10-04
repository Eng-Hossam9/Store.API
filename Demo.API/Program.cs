
using Demo.Core.Mapping;
using Demo.Core.RepositoriesInterFaces;
using Demo.Core.ServicesInterFaces;
using Demo.Repository.Data.Contexts;
using Demo.Repository.Data.DataSeed;
using Demo.Repository.Repositories;
using Demo.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreDbContext>(option => 
                           option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductService,ProductService>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(m=>m.AddProfile(new ProductProfile()));



            

            var app = builder.Build();


            using var Scope = app.Services.CreateScope();
            var service = Scope.ServiceProvider;
            var context = service.GetRequiredService<StoreDbContext>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                
                await context.Database.MigrateAsync();
                await SeedData.seedDataToDBAsync(context);
            }
            catch (Exception ex)     
            {
                var logger= loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,"Problem in Migrations");

            }

            // Configure the HTTP request pipeline. 
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

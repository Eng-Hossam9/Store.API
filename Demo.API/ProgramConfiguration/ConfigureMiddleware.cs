using Demo.API.Middlewares;
using Demo.Core.Models.Identity;
using Demo.Repository.Data.Contexts;
using Demo.Repository.Data.DataSeed;
using Demo.Repository.Identity.Context;
using Demo.Repository.Identity.DataSeed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.API.ProgramConfiguration
{
    public static class ConfigureMiddleware
    {
        public static async Task<WebApplication> addMiddleWarConfigurationAsync(this WebApplication app) 
        {
            app.UseMiddleware<ExceptionMiddleware>();

            using var Scope = app.Services.CreateScope();
            var service = Scope.ServiceProvider;
            var context = service.GetRequiredService<StoreDbContext>();
            var Identity = service.GetRequiredService<StoreIdentityDbContext>();
            var _userManagar = service.GetRequiredService<UserManager<AppUser>>();
            var _RoleManagar = service.GetRequiredService<RoleManager<IdentityRole>>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();


            try
            {

                await context.Database.MigrateAsync();
                await Identity.Database.MigrateAsync();
                await SeedData.seedDataToDBAsync(context);
                await SeedUser.SeedUserDataAsync(_userManagar, _RoleManagar);
                    }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Problem in Migrations");

            }



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithRedirects("/error/{0}");

            app.UseCors("AllowSpecificOrigin");


            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
    }
}

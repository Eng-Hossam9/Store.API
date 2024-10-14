using Demo.API.Middlewares;
using Demo.Repository.Data.Contexts;
using Demo.Repository.Data.DataSeed;
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
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();


            try
            {

                await context.Database.MigrateAsync();
                await SeedData.seedDataToDBAsync(context);
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

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
    }
}

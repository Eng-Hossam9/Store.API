
using Demo.API.Errors;
using Demo.API.Middlewares;
using Demo.API.ProgramConfiguration;
using Demo.Core.Mapping;
using Demo.Core.RepositoriesInterFaces;
using Demo.Core.ServicesInterFaces;
using Demo.Repository.Data.Contexts;
using Demo.Repository.Data.DataSeed;
using Demo.Repository.Repositories;
using Demo.Service.Services;
using Demo.Service.Services.Brand;
using Demo.Service.Services.Type;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         builder.Services.AddDependancy(builder.Configuration);




            var app = builder.Build();


          await  app.addMiddleWarConfigurationAsync();

            app.Run();
        }
    }
}

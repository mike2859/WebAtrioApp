using Microsoft.EntityFrameworkCore;
using WebAtrioApp.Application.Services;
using WebAtrioApp.Core.Interfaces;
using WebAtrioApp.Infrastructure.Data;
using WebAtrioApp.Infrastructure.Repositories;

namespace WebAtrioApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Ajout des services n�cessaires � l'application
            builder.Services.AddControllers();



            // Ajout du DbContext
            builder.Services.AddDbContext<WebAtrioDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




            // Ajout des services pour le projet, avec l'injection des d�pendances
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();
            builder.Services.AddScoped<PersonService>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Activation des contr�leurs API
            app.MapControllers();

            app.UseAuthorization();

            //var summaries = new[]
            //{
            //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            //};

            //app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            //{
            //    var forecast = Enumerable.Range(1, 5).Select(index =>
            //        new WeatherForecast
            //        {
            //            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //            TemperatureC = Random.Shared.Next(-20, 55),
            //            Summary = summaries[Random.Shared.Next(summaries.Length)]
            //        })
            //        .ToArray();
            //    return forecast;
            //})
            //.WithName("GetWeatherForecast")
            //.WithOpenApi();

            app.Run();
        }
    }
}

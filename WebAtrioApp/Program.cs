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

            // Ajout des services nécessaires à l'application
            builder.Services.AddControllers();



            // Ajout du DbContext
            builder.Services.AddDbContext<WebAtrioDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




            // Ajout des services pour le projet, avec l'injection des dépendances
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

            // Appliquez les migrations au démarrage
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebAtrioDbContext>();
                dbContext.Database.Migrate();  // Applique les migrations en cours
            }


            app.UseHttpsRedirection();

            // Activation des contrôleurs API
            app.MapControllers();

            app.UseAuthorization();


            app.Run();
        }
    }
}

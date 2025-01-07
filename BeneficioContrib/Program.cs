using BeneficioContrib.Cn.Database;
using Microsoft.EntityFrameworkCore;

namespace BeneficioContrib
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Contexto>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));

                if (builder.Environment.EnvironmentName == Environments.Development)
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                    options.LogTo(Console.WriteLine); // Escreve os comandos SQL executados pelo O.R.M. na janela de "Saída" do Visual Studio
                }
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization(); // Não tem login

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

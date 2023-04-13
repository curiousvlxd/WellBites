using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WellBites.DataAccess;
using WellBites.MVVM.ViewModels;
using WellBites.Views;
using WellBites.Models;
using WellBites.MVVM.Views;

namespace WellBites
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly ServiceCollection Services = new ServiceCollection();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
                    services.AddDbContext<WellBitesDbContext>(options =>
                        options.UseSqlServer(connectionString));
                    services.AddScoped<UserManagerService>();
                })
                .Build();
                var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<WellBitesDbContext>();
                var configuration = services.GetRequiredService<IConfiguration>();
                var userManagerService = services.GetRequiredService<UserManagerService>();
            //dbContext.Users.RemoveRange(dbContext.Users);
            //dbContext.SaveChanges();
            AuthPage authPage = new AuthPage(new UserViewModel(userManagerService));
            
            //DashboardPage page = new DashboardPage(new UserViewModel(userManagerService)); 
               new MainWindow(authPage, configuration.GetSection("api-keys")["x-api-key"]).Show();
        }
    }
}


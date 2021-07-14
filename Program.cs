using BolgSite.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BolgSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            try
            {
                var scope = host.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var usersManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");

                if (!context.Roles.Any())
                {
                    roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!context.Users.Any(u => u.UserName == "Admin"))
                {
                    var adminUser = new IdentityUser()
                    {
                        UserName = "admin",
                        Email = "admin@test.com"
                    };

                    usersManager.CreateAsync(adminUser, "Password").GetAwaiter().GetResult();
                    usersManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

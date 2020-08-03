using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstateApplication.Data.DatabaseContext.ApplicationDbContext;
using EstateApplication.Data.DatabaseContext.AuthenticationDbContext;
using EstateApplication.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EstateApplication.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(option => option.UseSqlServer
            (Configuration.GetConnectionString("ApplicationConnection"), sqlOptions =>
            {
                sqlOptions.MigrationsAssembly("EstateApplication.Data");
            }
            ));

            services.AddDbContextPool<AuthenticationDbContext>(option => option.UseSqlServer
            (Configuration.GetConnectionString("AuthenticationConnection"),
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly("EstateApplication.Data");
            }
            ));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireLowercase = false;
                option.SignIn.RequireConfirmedEmail = false;
                option.SignIn.RequireConfirmedPhoneNumber = false;

            });
            

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svp)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            MigrateDatabaseContext(svp);
            CreateDefaultRolesAndUsers(svp).GetAwaiter().GetResult();
        }

        public void MigrateDatabaseContext(IServiceProvider svp)
        {
            var authenticationDbContext = svp.GetRequiredService<AuthenticationDbContext>();
            authenticationDbContext.Database.Migrate();

            var applicationDbContext = svp.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();
        }

        public async Task CreateDefaultRolesAndUsers(IServiceProvider svp)
        {
            //Create an arrayh of the default roles needed
            string[] roles = new string[]
            {
                "SystemAdministrator",
                "Agent",
                "User"
            };

            var UserEmail = "admin@estateapplication.com";
            var UserPassword = "SuperSecretPassword2020";

            var roleManager = svp.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole {Name = role});
                }
            }

            var userManager = svp.GetService<UserManager<ApplicationUser>>();
            //Check if User role exist already else create one 
            var user = await userManager.FindByEmailAsync(UserEmail);

            //if role doesnt exist then create a new one
            if (user is null)
            {
                user = new ApplicationUser
                {
                    Email = UserEmail,
                    UserName = UserEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "+2348076261518",
                    PhoneNumberConfirmed = true
                };
                await userManager.CreateAsync(user, UserPassword);
                await userManager.AddToRolesAsync(user, roles); 
            }
        }
    }
}

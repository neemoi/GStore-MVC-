using GStore.Context;
using GStore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<GoogleStoreContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.LoginPath = new PathString("/Login/Login");
                     //options.AccessDeniedPath = new PathString("/Errors/404NotFound");
                 });

            builder.Services.AddIdentity<User, IdentityRole>()
             .AddEntityFrameworkStores<GoogleStoreContext>()
             .AddRoles<IdentityRole>();


            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //        .AddCookie(options =>
            //        {
            //            options.LoginPath = new PathString("/Login/Login");
            //            options.LogoutPath = new PathString("/Home/Index");
            //            options.AccessDeniedPath = new PathString("/AccessDenied/Acces");
            //        });

            //builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<GoogleStoreContext>();

            //builder.Services.AddScoped<SignInManager<User>>();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=UserPage}/{action=UserPageMain}/{id?}");

            app.Run();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.CustomValidations;
using AspNetCoreIdentity.Entity.DbContext;
using AspNetCoreIdentity.Entity.Entities;
using AspNetCoreIdentity.Requirement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreIdentity
{
    public class Startup
    {

        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(_=>_.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));


            //POLICY/CLAIMS
            services.AddTransient<IAuthorizationHandler, DateExchangeHandler>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AnkaraPolicy", policy =>
                {
                    policy.RequireClaim("City", "ankara");
                });

                opts.AddPolicy("ViolencePolicy", policy =>
                {
                    policy.RequireClaim("Violence");
                });

                opts.AddPolicy("ExchangePolicy", policy =>
                {
                    policy.AddRequirements(new DateExchangeRequirement());
                });
                
            });


            //IDENTITY CONF
            services.AddIdentity<AppUser, AppRole>(_ =>
                {
                    _.Password.RequireLowercase = false; //KÜÇÜK KARAKTER ÞARTI
                    _.Password.RequireDigit = false; //SAYI ÞARTI
                    _.Password.RequireNonAlphanumeric = false; //NON ALPHANUMERIC ÞARTI
                    _.Password.RequireUppercase = false; //BÜYÜK KARAKTER ÞARTI
                    _.Password.RequiredLength = 4; //UZUNLUK ÞARTI

                    _.User.RequireUniqueEmail = true; //EÞSÝZ EMAÝL ÞARTI
                    _.User.AllowedUserNameCharacters =
                        " abcçdefgðhýijklmnöopqrstüuvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSTUÜVWXYZ0123456789-._"; //KARAKTERLER

                }).AddPasswordValidator<CustomPasswordValidation>()
                .AddUserValidator<CustomUserValidation>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            


            //LOGÝN OLUNDUÐUNDA, COOKÝE OTOMATÝK KAYDEDÝLECEK.
            CookieBuilder cookieBuilder = new CookieBuilder
            {
                Name = "MyCookie",                               //COOKIE ISMÝ
                HttpOnly = false,                                //SADECE HTTP ÝSTEÐÝNDE COOKÝE'YE ERÝÞ, CLIENT DA ERÝÞÝLEMESÝN
                SameSite = SameSiteMode.Lax,                     //SADECE O SÝTEDEN ÝSTEK GELDÝÐÝNDE COOKÝE'YE ERÝÞ
                SecurePolicy = CookieSecurePolicy.SameAsRequest  //ALWAYS --> HTTPS ,SAMEASREQUEST --> HHTP-HTTPS, NONE-->HTTP
            };


            //COOKIE CONF
            services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = new PathString("/Home/Login");    //SÝTENÝN LOGÝN SAYFASI(GÝRÝÞ YAPMAMIÞSA OTOMATÝK YÖNLENDÝRÝR.)
                opts.LogoutPath = new PathString("/Member/Logout");  //SÝTENÝN LOGOUT SAYFASI
                opts.AccessDeniedPath = new PathString("/Home/AccessDenied");
                opts.Cookie = cookieBuilder;                       //AYARLARI
                opts.SlidingExpiration = true;                     //HER LOGÝN OLDUÐUNDA SÜREYÝ SIFIRLA
                opts.ExpireTimeSpan = TimeSpan.FromDays(60);

            });


            services.AddScoped<IClaimsTransformation, ClaimProvider.ClaimProvider>();
            services.AddMvc(_=>_.EnableEndpointRouting = false);
        }

        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();  //ALDIÐIMIZ HATAYLA ÝLGÝLÝ AÇIKLAYICI BÝLGÝLER
            app.UseStatusCodePages();         //ÝÇERÝK DÖNMEYEN SAYFALARDA BÝZÝ BÝLGÝLENDÝRÝR
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();          //IDENTTIY YAPISI ÝÇÝN
            app.UseMvc(configureRoutes);
            
        }


        private void configureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("default", "{Controller=Home}/{Action=Index}/{id?}");
        }
    }
}

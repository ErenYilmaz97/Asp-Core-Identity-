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
                    _.Password.RequireLowercase = false; //K���K KARAKTER �ARTI
                    _.Password.RequireDigit = false; //SAYI �ARTI
                    _.Password.RequireNonAlphanumeric = false; //NON ALPHANUMERIC �ARTI
                    _.Password.RequireUppercase = false; //B�Y�K KARAKTER �ARTI
                    _.Password.RequiredLength = 4; //UZUNLUK �ARTI

                    _.User.RequireUniqueEmail = true; //E�S�Z EMA�L �ARTI
                    _.User.AllowedUserNameCharacters =
                        " abc�defg�h�ijklmn�opqrst�uvwxyzABC�DEFG�HI�JKLMNO�PQRSTU�VWXYZ0123456789-._"; //KARAKTERLER

                }).AddPasswordValidator<CustomPasswordValidation>()
                .AddUserValidator<CustomUserValidation>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            


            //LOG�N OLUNDU�UNDA, COOK�E OTOMAT�K KAYDED�LECEK.
            CookieBuilder cookieBuilder = new CookieBuilder
            {
                Name = "MyCookie",                               //COOKIE ISM�
                HttpOnly = false,                                //SADECE HTTP �STE��NDE COOK�E'YE ER��, CLIENT DA ER���LEMES�N
                SameSite = SameSiteMode.Lax,                     //SADECE O S�TEDEN �STEK GELD���NDE COOK�E'YE ER��
                SecurePolicy = CookieSecurePolicy.SameAsRequest  //ALWAYS --> HTTPS ,SAMEASREQUEST --> HHTP-HTTPS, NONE-->HTTP
            };


            //COOKIE CONF
            services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = new PathString("/Home/Login");    //S�TEN�N LOG�N SAYFASI(G�R�� YAPMAMI�SA OTOMAT�K Y�NLEND�R�R.)
                opts.LogoutPath = new PathString("/Member/Logout");  //S�TEN�N LOGOUT SAYFASI
                opts.AccessDeniedPath = new PathString("/Home/AccessDenied");
                opts.Cookie = cookieBuilder;                       //AYARLARI
                opts.SlidingExpiration = true;                     //HER LOG�N OLDU�UNDA S�REY� SIFIRLA
                opts.ExpireTimeSpan = TimeSpan.FromDays(60);

            });


            services.AddScoped<IClaimsTransformation, ClaimProvider.ClaimProvider>();
            services.AddMvc(_=>_.EnableEndpointRouting = false);
        }

        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();  //ALDI�IMIZ HATAYLA �LG�L� A�IKLAYICI B�LG�LER
            app.UseStatusCodePages();         //��ER�K D�NMEYEN SAYFALARDA B�Z� B�LG�LEND�R�R
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();          //IDENTTIY YAPISI ���N
            app.UseMvc(configureRoutes);
            
        }


        private void configureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("default", "{Controller=Home}/{Action=Index}/{id?}");
        }
    }
}

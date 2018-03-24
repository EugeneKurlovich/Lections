﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lections.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lections
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

    
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст MobileContext в качестве сервиса в приложение
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connection));

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
          .AddFacebook(Foptions =>
          {
              Foptions.AppId = "783478808509094";
              Foptions.AppSecret = "603a11fb352f6693d9185538a49943fa";
          })
          .AddTwitter(Toptions =>
          {
              Toptions.ConsumerKey = "K4rGUY6SwKPcvCcm73TNFDMgr";
              Toptions.ConsumerSecret = "7v87qSbvOu4KIAVoVzwgYgN3q3oEutE0QVjz2xnrz9CJGXSIzK";
          })
          .AddGoogle(Goptions =>
          {
              Goptions.ClientId = "996213987184-bqbklu8h12v8dtepvamr0j86sqvs0trj.apps.googleusercontent.com";
              Goptions.ClientSecret = "LPDI1NPl3fOjE1g6TBgU_qae";
          }
          )
          .AddCookie();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

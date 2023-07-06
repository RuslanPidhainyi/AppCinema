using AppCinema.Data;
using AppCinema.Data.Cart;
using AppCinema.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http; //for IHttpContextAccessor
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema
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

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //1 step - configuration

            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(Configuration["Data:Connection"]));//.GetConnectionString("DefaultConnectionString"))); //How dataStory(DataBase) that it is going to use. We a going to configuration sql server  

            //2 step - Services configuration
            services.AddScoped<IActorsService, ActorsService>();
            services.AddScoped<IProducersService, ProducersService>();
            services.AddScoped<ICinemasService, CinemasService>();
            services.AddScoped<IMoviesService, MoviesService>();

            
            //3 step ( And downstairs )

            //� �������������� ����� add single tour, �� ������ ������ ����� ����� ����.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(e => ShoppingCart.GetShoppingCart(e));


            services.AddSession();


            //Seed DB
            //AppDbInitializer.Seed(app);



            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //step 3.1
            app.UseSession();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Seed DB
            AppDbInitializer.Seed(app);
        }
    }
}

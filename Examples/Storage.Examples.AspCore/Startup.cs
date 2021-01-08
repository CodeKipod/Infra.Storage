using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore;
using Storage.Examples.AspCore.Entities;
using Storage.Examples.AspCore.Repositories;

namespace Storage.Examples.AspCore
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
            //services.RegiserScoppedDbContextOf<PeopleDbContext>();
            //services.AddDbContext<PeopleDbContext>();
            ////services.AddScoped<IDbContextProvider, DbContextProvider>();
            //services.AddScoped<EFCorePeopleRepository>();

            services.RegisterSingleKeyRepositoryFor<int, Person, PeopleDbContext>();



            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

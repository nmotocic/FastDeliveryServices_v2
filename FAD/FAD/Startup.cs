using FAD.Domain.Repository;
using FAD.Domain.Services;
using FAD.Models;
using FAD.Repository;
using FAD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FAD
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FAD", Version = "v1" });
            });

            // Added - uses IOptions<T> for your settings.
            services.AddOptions();

            // Added - Confirms that we have a home for our DataConnection
            services.Configure<DataConnection>(Configuration.GetSection("DataConnection"));

            //services.AddTransient<IFlightRepository>(s => new FlightRepository("DRIVER={SQL Server}; SERVER=localhost; DATABASE=FAD DB; Trusted_Connection=yes"));

            services.AddSingleton<IFlightRepository>(new FlightRepository("DRIVER={SQL Server}; SERVER=localhost; DATABASE=FAD DB; Trusted_Connection=yes"));

            //services.AddTransient<IFlightRepository, FlightRepository>();
            services.AddSingleton<IDeliveryRepository, DeliveryRepository>();

            services.AddScoped<IFlightService, FlightService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Glossary v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}


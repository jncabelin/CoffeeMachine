using CoffeeMachine.Api.Services.Interfaces;
using CoffeeMachine.Api.Dtos.Requests;
using Microsoft.OpenApi.Models;
using CoffeeMachine.Api.Services;

namespace CoffeeMachine.Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ICoffeeMachineService, FakeCoffeeMachineService>();
            services.AddSingleton<IWeatherMapService, OpenWeatherMapService>();
            services.AddSingleton<IDateTimeProviderService, DateTimeProviderService>();
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining(typeof(BrewCoffeeQuery)));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Coffee Machine API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coffee Machine API V1");
                c.RoutePrefix = string.Empty;
            });

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
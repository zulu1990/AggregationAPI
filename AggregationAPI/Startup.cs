using AggretationApp.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AggregationAPI
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
            //var server = Configuration["DbServer"] ?? "localhost";
            //var port = Configuration["DbPort"] ?? "1450";
            //var user = Configuration["DbUser"] ?? "SA";
            //var passwrod = Configuration["Password"] ?? "P@$$word";
            //var database = Configuration["Database"] ?? "AggregationAPI";
            //var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={passwrod}";



            services.AddDbContext<DataContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DockerDb")));


            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AggregationAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AggregationAPI v1");
                });
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
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

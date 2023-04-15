using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SVS.Invoice.Api.Jobs;
using SVS.Invoice.Models.Data;
using SVS.Invoice.Models.Profiles;
using SYS.Invoice.BLL.HelperServices;
using SYS.Invoice.BLL.Infrastructure;
using SYS.Invoice.BLL.Models;
using System.IO;

namespace SVS.Invoice.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services
                .AddDbContext<AppDbContext>(x => x.UseSqlServer(_config.GetConnectionString("DataConnection")));

            services.AddScoped<IBaseRepo, BaseRepo>();
            services.AddScoped<IFileOperationsService, FileOperationsService>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new InvoiceProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHangfire(configuration => {
                configuration.UseSqlServerStorage(_config.GetConnectionString("DataConnection"));
                RecurringJob.AddOrUpdate<HangfireJob>(x => x.LoadRecords(), "*/30 * * * *");
            });

            services.AddHangfireServer();

            services.Configure<MailSettings>(_config.GetSection("EmailSettings"))
                        .AddScoped<IEmailSender, EmailSender>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SVS.Invoice.Api", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "SVS.Invoice.Api.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SVS.Invoice.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard("/dashboard/hangfire", new DashboardOptions
            {
                DashboardTitle = "SVS hangfire dashboard"

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShopRUs.Core;
using ShopRUs.Core.Interfaces;
using ShopRUs.Core.Services;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace ShopRUs.Api
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
            services.AddControllers();
            services.AddDbContext<ShopRUsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ShopRUsContext")));
            services.AddScoped<ICustomer, CustomerService>();
            services.AddScoped<IDiscount, DiscountService>();
            services.AddScoped<IPayment, PaymentService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopRUs.API", Version = "v1" });
                /*  var securityScheme = new OpenApiSecurityScheme
                  {
                      Name = "Resqu API JWT",
                      Description = "Enter JWT Bearer token **_only_**",
                      In = ParameterLocation.Header,
                      Type = SecuritySchemeType.Http,
                      Scheme = "bearer", // must be lower case
                      BearerFormat = "JWT",
                      Reference = new OpenApiReference
                      {
                          Id = JwtBearerDefaults.AuthenticationScheme,
                          Type = ReferenceType.SecurityScheme
                      }
                  };
                  c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                  c.AddSecurityRequirement(new OpenApiSecurityRequirement
                  {
                      {securityScheme, new string[] { }}
                  });*/
                c.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();

            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopRUs.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseCors("AllowAll");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

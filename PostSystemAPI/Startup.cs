using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.DAL.Repository;
using PostSystemAPI.Domain.Configuration;
using PostSystemAPI.Domain.Services.Implementations;
using PostSystemAPI.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSystemAPI.Domain.Commands;
using PostSystemAPI.Domain.Queries;
using PostSystemAPI.SignalR;

namespace PostSystemAPI
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
            services.AddDbContext<PostSystemContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("PostSystemConnection")));
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            //Authentication and scheme
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // this will validate the 3rd part of the jwt token using the secret that we added in the appsettings and verify we have generated the jwt token
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Add the secret key to our Jwt encryption
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["JwtConfig:validIssuer"],
                    ValidAudience = Configuration["JwtConfig:validAudience"]
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("WorkerPolicy", policy => policy.RequireRole("Viewer"));
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("DriverPolicy", policy => policy.RequireRole("Driver"));
            });

            services.AddIdentity<User,IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PostSystemContext>();

            services.AddSignalR();

            services.AddScoped<ICityRepo, CityRepo>();
            services.AddScoped<IPostOfficeRepo, PostOfficeRepo>();
            services.AddScoped<IDeliveryRepo, DeliveryRepo>();
            services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();
            //services.AddScoped<ISenderRepo, SenderRepo>();
            //services.AddScoped<IReceiverRepo,ReceiverRepo>();

           // services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPostOfficeService, PostOfficeService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            //services.AddScoped<ISenderService, SenderService>();
            //services.AddScoped<IReceiverService, ReceiverService>();
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetDriverDeliveriesQuery>());
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<AssignDeliveryToDriverCommand>());
            //services.AddMediatR(c => typeof(Startup).GetTypeInfo().Assembly);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PostSystemAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options
               .WithOrigins("http://localhost:4200", "http://localhost:8100", "http://localhost:56460")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PostSystemAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<LiveMapHub>("/liveMapHub");
            });
        }
    }
}

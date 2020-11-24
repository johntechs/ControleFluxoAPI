using ControleFluxoAPI.Domain.Repositories;
using ControleFluxoAPI.Domain.Services;
using ControleFluxoAPI.Filters;
using ControleFluxoAPI.Persistence.Contexts;
using ControleFluxoAPI.Persistence.Repositories;
using ControleFluxoAPI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;
using System;

namespace ControleFluxoAPI
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers(options => 
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(options =>
                
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
            })
            .ConfigureApiBehaviorOptions( options => {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddDbContext<ControleFluxoDBContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DevConnection"));
                options.UseLazyLoadingProxies();
            });

            services.AddMvc(options => {

                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options => {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
            services.AddScoped<IAgendamentoService, AgendamentoService>();

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControleFluxoAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(options =>
                options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()

            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleFluxoAPI v1"));
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

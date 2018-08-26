using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contexto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modelo.Streaming;
using Servicios;

namespace QueMovieApi
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
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddMvc();

            var connection = Configuration.GetConnectionString("Conexion");
            services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connection));

            //Mis servicios
            services.AddTransient<IUsuarioServicio, UsuarioServicio>();
            services.AddTransient<ISerieServicio, SerieServicio>();
            services.AddTransient<ICapituloServicio, CapituloServicio>();
            services.AddTransient<IGeneroServicio, GeneroServicio>();
            services.AddTransient<IUsuarioSerieServicio, UsuarioSerieServicio>();
            services.AddTransient<IGeneroSerieServicio, GeneroSerieServicio>();
            services.AddTransient<IUsuarioCapituloServicio, UsuarioCapituloServicio>();

            //Configuración de tamaño limite de mis archivos de subida 3Gb.
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });
            
            //Permiso Ajax
            services.AddCors(options => 
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowAnyOrigin()
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos")),
            //    RequestPath = "/MyVideos"
            //});

            app.UseCors("AllowSpecificOrigin");
            app.UseMvc();
        }
    }
}

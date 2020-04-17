using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAPI.Data;
using FirstAPI.Business;
using FirstAPI.Business.Implementattion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FirstAPI.Repository.Implementattions;
using FirstAPI.Repository;

namespace FirstAPI
{
    public class Startup
    {
        //add migrations addicionas duas dependencias do nuget: Evolve e MySqlData. 
        //add atributos ILogger e IHostingEnvironment
        private readonly ILogger _logger;
        public IHostingEnvironment _environment { get; }
        public IConfiguration _configuration { get; }


        public Startup(IConfiguration configuration, ILogger<Startup> logger, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _logger = logger;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //conecçao ao db
            var connectionString = _configuration["MySqlConnection:MySqlConnectionStrings"];
            services.AddDbContext<FirstAPIContext>(options => options.UseMySql(connectionString));

            //verifica em qual ambiente esta executando a migration
            if (_environment.IsDevelopment())
            {
                //addciona novas tags no arquivo firstapi.csproj para conectar de duas formas no db
                try
                {
                    var evolveConnetion = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve(evolveConnetion, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        //setar false para apagar confgs do dp
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Data base migration failed.", ex);
                    throw;
                }
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //versionamento(baixar nuget microsot.aspnetcore.mvc.versioning
            services.AddApiVersioning();


            //injeçoes de deperndencia
            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

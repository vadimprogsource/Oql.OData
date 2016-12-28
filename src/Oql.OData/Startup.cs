using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oql.Linq.Api.CodeGen;
using Oql.Linq.Infrastructure.Metadata;
using Oql.Linq.Infrastructure.CodeGen;
using Oql.Linq.Api.Data;
using Oql.Linq.Api.Query;
using Oql.Linq.Infrastructure.Query;
using OData.Linq.Infrastucture.Channels;
using OData.Linq.Api;
using OData.Linq.Infrastucture;
using OData.Linq.Infrastucture.Parsing;

namespace Oql.OData
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        public string ConnectionString { get; } = "Data Source =.; Initial Catalog = crm; User ID = sa; Password = 1; Connection Timeout = 18";

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICodeProvider>(x => new CodeProvider());
            services.AddScoped<IDataProvider>   (x => new MsSql.SqlDataProvider(ConnectionString));
            services.AddScoped<IObjectQueryProvider, ObjectQueryProvider>();

            services.AddScoped<IODataChannelFactory, ODataChannelFactory>();
            services.AddScoped<IODataQueryProvider , ODataQueryProvider> ();
            services.AddScoped<IODataQueryParser   , ODataQueryParser>   ();


            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();
        }
    }
}

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Entity;
using StackQuestion.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Logging;
using StackQuestion.Repositories;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Routing;

namespace StackQuestion
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<StackQuestionContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<StackQuestionContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddTransient<Repository<Question>, QuestionRepository>();
            services.AddTransient<Repository<Tag>, TagRepository>();
            services.AddTransient<Repository<Answer>, AnswerRepository>();
            services.AddTransient<IUrlHelper, UrlHelper>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseIISPlatformHandler();

            app.UseIdentity();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "questions/tagged/{tag}/page{page}/size{size}",
                    defaults: new { controller = "Question", action = "List" },
                    constraints: new { tag = @"[a-z]*[a-z.-0-9]*[a-z]*", page = @"\d+", size = @"\d+" });

                routes.MapRoute(
                    name: null,
                    template: "questions/tagged/{tag}/page{page}",
                    defaults: new { controller = "Question", action = "List"},
                    constraints: new { tag = @"[a-z]*[a-z.-0-9]*[a-z]*", page = @"\d+" });

                routes.MapRoute(
                    name: null, 
                    template: "questions/tagged/{tag}", 
                    defaults: new { controller = "Question", action = "List", page = 1 }, 
                    constraints: new { tag = @"[a-z]*[a-z.-0-9]*[a-z]*"});

                routes.MapRoute(
                    name: null,
                    template: "questions/page{page}/size{size}",
                    defaults: new { controller = "Question", action = "List" },
                    constraints: new { page = @"\d+", size = @"\d+" });

                routes.MapRoute(
                    name: null, 
                    template: "questions/page{page}", 
                    defaults: new { controller = "Question", action = "List" }, 
                    constraints: new { page = @"\d+"});

                routes.MapRoute(
                    name: null,
                    template: "question/{id}",
                    defaults: new { controller = "Question", action = "Detail", page = 1, size = 10 },
                    constraints: new { id = @"\d+"});

                routes.MapRoute(
                    name: null,
                    template: "questions",
                    defaults: new { controller = "Question", action = "List", page = 1 });

                routes.MapRoute(
                    name: null,
                    template: "{controller=Question}/{action=List}"
                    );
            });
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

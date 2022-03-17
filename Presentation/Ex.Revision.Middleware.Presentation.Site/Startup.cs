using Ex.Revision.Middleware.Presentation.Site.Middleware;
using Ex_Revision_Middleware.Business;
using Ex_Revision_Middleware.Data;
using Ex_Revision_Middleware.Data.Providers.Sql;
using Ex_Revision_Middleware.Data.Providers.Sql.Models;
using Ex_Revision_Middleware.Data.Providers.Sql.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Ex.Revision.Middleware.Presentation.Site
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
            string connectionString = this.Configuration.GetConnectionString("FormationCenter");
            //services.AddDbContext<RevisionEtMiddlewareDbContext>(options => options.UseSqlServer("Server=LAPTOP-A6J103NA\\FORMATION;Database=FormationCenter;Integrated Security=true;"));
            services.AddDbContext<RevisionEtMiddlewareDbContext>(options => options.UseSqlServer(connectionString));

            /*services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<Role>()
                .AddEntityFrameworkStores<RevisionEtMiddlewareDbContext>();

            services.AddIdentityServer().AddApiAuthorization<User, RevisionEtMiddlewareDbContext>();*/

            string swaggerVersion = this.Configuration.GetSection("Swagger:Version").Value;
            string swaggerTitle = this.Configuration.GetSection("Swagger:Title").Value;

            /*services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RevisionEtMiddleWare",
                    Version = "v1"
                });
            });*/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerVersion, new OpenApiInfo
                {
                    Title = swaggerTitle,
                    Version = swaggerVersion
                });
            });

            services.AddControllersWithViews();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<CourseDomain>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<StudentDomain>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserDomain>();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RevisionEtMiddleWare v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseMyCustomMiddleware();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}

namespace PocDotNet5.Api
{
    using Data.EntityFramework;
    using Data.Repositories;
    using Domain.Repositories;
    using FluentValidation.AspNetCore;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Schemas.Responses;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services
                .AddDbContext<PocDotNet5Context>(opt =>
                    opt.UseSqlServer(Configuration["ConnectionString"]))
                .AddAutoMapper(typeof(Startup))
                .AddMediatR(typeof(Startup))
                .AddControllers()
                .AddFluentValidation(cfg =>
                    cfg.RegisterValidatorsFromAssemblyContaining<Startup>())
                .ConfigureApiBehaviorOptions(opt =>
                    opt.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var modelState = actionContext.ModelState;
                        return new UnprocessableEntityObjectResult(new ValidationErrorsResponse(modelState));
                    });

            services
                .AddApiVersioning(opt =>
                {
                    opt.ReportApiVersions = true;
                    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddVersionedApiExplorer(opt =>
                {
                    opt.GroupNameFormat = "'v'VVV";
                    opt.SubstituteApiVersionInUrl = true;
                })
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        opt.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
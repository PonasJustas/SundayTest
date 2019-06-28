using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Owin;
using Sunday.Repositories;
using Sunday.Services;
using Sunday.WebApi.Contracts;
using Sunday.WebApi.Host.Filters;
using Swashbuckle.Application;
using System.Reflection;
using System.Web.Http;

namespace Sunday.WebApi.Host.Configuration
{
    public class StartupConfig
    {
        public void Configure(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Sunday API");
                    c.DescribeAllEnumsAsStrings();
                    c.OperationFilter<FileUploadOperation>();
                }).EnableSwaggerUi(c => { });

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(config);

            RegisterRepositories(builder);
            RegisterServices(builder);
            builder.RegisterInstance(SetupMaps());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<SundayContext>().AsSelf().WithParameter(new TypedParameter(
                typeof(DbContextOptions<SundayContext>),
                new DbContextOptionsBuilder<SundayContext>().UseInMemoryDatabase(databaseName: "SundayDb").Options));
            builder.RegisterType<MunicipalityRepository>().AsImplementedInterfaces();
            builder.RegisterType<TaxRepository>().AsImplementedInterfaces();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<MunicipalityService>().AsSelf().PropertiesAutowired();
            builder.RegisterType<TaxService>().AsSelf().PropertiesAutowired();
        }

        private IMapper SetupMaps()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Repositories.Entities.Municipality, Municipality>().ReverseMap();
                    cfg.CreateMap<Repositories.Entities.Tax, Tax>().ReverseMap();
                });

            return config.CreateMapper();
        }
    }
}

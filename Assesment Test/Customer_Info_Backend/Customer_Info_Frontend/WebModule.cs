using Autofac;
using Customer_Info_Frontend.ViewModels;

namespace Customer_Info_Frontend
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        //private readonly IConfiguration _configuration;

        public WebModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
            //_configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CountryVM>().AsSelf();
            base.Load(builder);
        }
    }
}

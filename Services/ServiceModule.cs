using Ninject.Modules;
using Microsoft.EntityFrameworkCore;
namespace EF_lab4.Services
{
    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionString;


        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<DbContextOptions<MyDbContext>>()
                .ToMethod(context =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
                    optionsBuilder.UseSqlServer(_connectionString);
                    return optionsBuilder.Options;
                })
                .InSingletonScope();

            Bind<MyDbContext>().ToSelf().InTransientScope();

            Bind<IBookServices>().To<BookServices>().InTransientScope();
            Bind<IAuthorsServices>().To<AuthorsServices>().InTransientScope();
            Bind<IPublishingHouseServices>().To<PublishingHouseServices>().InTransientScope();
        }
    }
}

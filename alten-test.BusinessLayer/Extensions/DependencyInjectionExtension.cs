using AutoMapper;
using alten_test.Core.Mapping;
using alten_test.DataAccessLayer.Context;
using alten_test.DataAccessLayer.Interfaces;
using alten_test.DataAccessLayer.Repositories;
using Unity;
using Unity.Extension;

namespace alten_test.BusinessLayer.Utilities
{
    public class DependencyInjectionExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            /*Container.RegisterType(
                typeof(IDatabaseContextFactory),
                typeof(DatabaseContextFactory),
                null,
                TypeLifetime.Transient);*/
            
            Container.RegisterType(
                typeof(IUnitOfWork),
                typeof(UnitOfWork),
                null,
                TypeLifetime.Transient);

            var mapperConfig = new MapperConfiguration(config =>
                config.AddProfile<MappingProfile>());
            IMapper mapper = mapperConfig.CreateMapper();
            
            Container.RegisterInstance(
                typeof(IMapper), 
                null, 
                mapper, 
                InstanceLifetime.Singleton);

        }
    }
}
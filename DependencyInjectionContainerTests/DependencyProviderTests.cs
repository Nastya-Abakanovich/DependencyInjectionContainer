using DependencyInjectionContainerLibrary;

namespace DependencyInjectionContainerTests
{
    public  class DependencyProviderTests
    {

        [Test]
        public void Resolve_InterfaceImplementation_RequiredTypeObject()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register(typeof(IService), typeof(ServiceImplInt));

            DependencyProvider provider = new DependencyProvider(configuration);
            var service = provider.Resolve<IService>();

            Assert.AreEqual(service.GetType(), typeof(ServiceImplInt));
        }

        [Test]
        public void Resolve_AbstractImplementation_RequiredTypeObject()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register(typeof(AbstractService), typeof(ServiceImplAbstr));

            DependencyProvider provider = new DependencyProvider(configuration);
            var service = provider.Resolve<AbstractService>();

            Assert.AreEqual(service.GetType(), typeof(ServiceImplAbstr));
        }

        [Test]
        public void Resolve_AsSelfImplementation_RequiredTypeObject()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register(typeof(ServiceImplInt), typeof(ServiceImplInt));

            DependencyProvider provider = new DependencyProvider(configuration);
            var service = provider.Resolve<ServiceImplInt>();

            Assert.AreEqual(service.GetType(), typeof(ServiceImplInt));
        }

        [Test]
        public void Resolve_Recursion_RequiredTypeObject()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register< IComplexService, ComplexServiceImpl>();
            configuration.Register<IRepository, RepositoryImpl>();

            DependencyProvider provider = new DependencyProvider(configuration);
            var complexService = provider.Resolve<IComplexService>();
            ComplexServiceImpl serviceImpl = (ComplexServiceImpl)complexService;

            Assert.AreEqual(complexService.GetType(), typeof(ComplexServiceImpl));
            Assert.AreEqual(serviceImpl.Repository.GetType(), typeof(RepositoryImpl));
        }

        [Test]
        public void Resolve_WithoutConfiguration_Exception()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();

            DependencyProvider provider = new DependencyProvider(configuration);
            Assert.Throws<Exception>(() => provider.Resolve<IService>());
        }

        [Test]
        public void Resolve_LifeTime_InstancePerDependency()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register<IService, ServiceImplInt>(LifeTime.InstancePerDependency);

            DependencyProvider provider = new DependencyProvider(configuration);
            var impl1 = provider.Resolve<IService>();
            var impl2 = provider.Resolve<IService>();

            Assert.AreNotSame(impl1, impl2);
        }

        [Test]
        public void Resolve_LifeTime_Singleton()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register<IService, ServiceImplInt>(LifeTime.Singleton);

            DependencyProvider provider = new DependencyProvider(configuration);
            var impl1 = provider.Resolve<IService>();
            var impl2 = provider.Resolve<IService>();

            Assert.AreSame(impl1, impl2);
        }


        [Test]
        public void Resolve_MultipleImplementations_RequiredTypeObject()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register(typeof(IService), typeof(ServiceImplInt));
            configuration.Register(typeof(IService), typeof(ServiceImplIntSecond));

            DependencyProvider provider = new DependencyProvider(configuration);
            var services = provider.Resolve <IEnumerable<IService>>();

            Assert.AreEqual(services.ElementAt(0).GetType(), typeof(ServiceImplInt));
            Assert.AreEqual(services.ElementAt(1).GetType(), typeof(ServiceImplIntSecond));
        }

        [Test]
        public void Resolve_TemplateTypeGenerics_RequiredTypeObject()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register<IRepository, RepositoryImpl>();
            configuration.Register<IGenService<IRepository>, GenServiceImpl<IRepository>>();

            DependencyProvider provider = new DependencyProvider(configuration);
            var services = provider.Resolve<IGenService<IRepository>>();

            Assert.AreEqual(services.GetType(), typeof(GenServiceImpl<IRepository>));
        }

        [Test]
        public void Resolve_TemplateType2Typeof_RequiredTypeObject()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register<IRepository, RepositoryImpl>();
            configuration.Register(typeof(IGenService<>), typeof(GenServiceImpl<>));

            DependencyProvider provider = new DependencyProvider(configuration);
            var services = provider.Resolve<IGenService<IRepository>>();

            Assert.AreEqual(services.GetType(), typeof(GenServiceImpl<IRepository>));
        }

    }
}

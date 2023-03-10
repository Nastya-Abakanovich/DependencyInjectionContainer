using System.Collections.Concurrent;

namespace DependencyInjectionContainerLibrary
{
    public class DependenciesConfiguration
    {
        public readonly ConcurrentDictionary<Type, List<ImplementationInfo>> RegisteredDependencies;

        public DependenciesConfiguration()
        {
            RegisteredDependencies = new ConcurrentDictionary<Type, List<ImplementationInfo>>();
        }

        public void Register<TDependency, TImplementation>(LifeTime lifeTime = LifeTime.InstancePerDependency)
        {
            Register(typeof(TDependency), typeof(TImplementation), lifeTime);
        }

        public void Register(Type interfaceType, Type classType, LifeTime lifeTime = LifeTime.InstancePerDependency)
        {
            if ( classType.IsAbstract || (!interfaceType.IsAssignableFrom(classType) && !interfaceType.IsGenericTypeDefinition)) 
            {
                throw new Exception("Dependency registration exception");
            }

            if (!RegisteredDependencies.ContainsKey(interfaceType))
            {
                List<ImplementationInfo> impl = new List<ImplementationInfo> { new ImplementationInfo(lifeTime, classType) };
                RegisteredDependencies.TryAdd(interfaceType, impl);
            }
            else
            {
                ImplementationInfo newImplInfo = new ImplementationInfo(lifeTime, classType);
                if (RegisteredDependencies[interfaceType].Where(implInf => newImplInfo.Equals(implInf)).Count() == 0)
                    RegisteredDependencies[interfaceType].Add(newImplInfo);
            }
        }
    }
}
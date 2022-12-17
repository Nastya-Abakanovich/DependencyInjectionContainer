namespace DependencyInjectionContainerLibrary
{
    public class ImplementationInfo
    {
        public Type ImplClassType;

        public LifeTime LifeTime;

        public ImplementationInfo(LifeTime lifeTime, Type implClassType)
        {
            ImplClassType = implClassType;
            LifeTime = lifeTime;
        }
    }
}

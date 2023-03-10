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

        public override bool Equals(object? obj)
        {
            return obj is ImplementationInfo info &&
                   EqualityComparer<Type>.Default.Equals(ImplClassType, info.ImplClassType) &&
                   LifeTime == info.LifeTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ImplClassType, LifeTime);
        }
    }
}

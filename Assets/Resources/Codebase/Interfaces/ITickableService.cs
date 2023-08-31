using Zenject;

namespace Infrastructure
{
    public interface ITickableService : ITickable
    {
        public void SubscribeDelegateToTick(TickDelegate action);
        public void DeleteDelegateFromTick(TickDelegate action);
    }
}
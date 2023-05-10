using Zenject;

namespace Infrastructure
{
    public interface ITickableService : ITickable
    {
        public void AddToTick(TickDelegate action);
        public void DeleteFromTick(TickDelegate action);
    }
}
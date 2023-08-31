using Zenject;

namespace Infrastructure
{
    public delegate void TickDelegate();
    public  class TickableService : ITickable
    {       
        public TickDelegate tickDelegate;
        public void Tick() => tickDelegate?.Invoke();
        public void AddToTick(TickDelegate action) => tickDelegate += action;
        public void DeleteFromTick(TickDelegate action) => tickDelegate -= action;
    }
}
using System;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public delegate void TickDelegate();
    public  class TickableService : ITickable
    {
        
        public TickDelegate tickDelegate;
        public void Tick()
        {
            tickDelegate.Invoke();
            Debug.Log("tick");
        }
        public void AddToTick(TickDelegate action) => tickDelegate += action;
        public void DeleteFromTick(TickDelegate action) => tickDelegate -= action;
    }
}
using UnityEngine;

namespace Codebase.Infrastructure
{
    public class TimeService
    {
        public void StopGameTime() => Time.timeScale = 0f;
        public void StartGameTime() => Time.timeScale = 1f;
    }
}
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure
{
    public class PlayerInput
    {
        ShipBehaviour _shipBehaviour;
        CoroutineProcessor _coroutineProcessor;
        UiService _uiController;
        TickDelegate _tickDelegate;
        [Inject]
        public void Construct(ShipBehaviour shipBehaviour, CoroutineProcessor coroutineProcessor, UiService uiController, TickableService tickableService)
        {
            _shipBehaviour = shipBehaviour;
            _coroutineProcessor = coroutineProcessor;
            _uiController = uiController;
            _tickDelegate += Tick;
            tickableService.AddToTick(Tick);
        }

        private void Tick()
        {
            if (IsMenuCalled()) _uiController.ToggleGameMenu();

            _shipBehaviour.RotateToMouse(GetMousePosition());

            if (IsAcelerating()) _shipBehaviour.Accelerate();
            else if (IsDecelerating()) _shipBehaviour.Decelerate();

            if (IsFiring()) _shipBehaviour.Shoot(_shipBehaviour.projectileInfos[0]);
        }


        Vector3 GetMousePosition()
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0;
            return mousepos;
        }
        bool IsMenuCalled() => Input.GetKey(KeyCode.A);
        bool IsAcelerating() => Input.GetKey(KeyCode.A);
        bool IsDecelerating() => Input.GetKey(KeyCode.D);
        bool IsFiring() => Input.GetMouseButton(0);

    }
}
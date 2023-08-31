using Infrastructure;
using UnityEngine;

namespace Services
{
    public class PlayerInputService
    {
        ShipBehaviour _shipBehaviour;
        UiService _uiController;

        public PlayerInputService(ShipBehaviour shipBehaviour, CoroutineProcessorService coroutineProcessor, UiService uiController, TickableService tickableService)
        {
            _shipBehaviour = shipBehaviour;
            _uiController = uiController;
            tickableService.AddToTick(Tick);
        }


        private void Tick()
        {
            _shipBehaviour.RotateToPointer(GetMousePosition());


            if (IsMenuCalled())
                _uiController.ToggleGameMenu();

            if (IsAcelerating())
                _shipBehaviour.Accelerate();

            else if (IsDecelerating())
                _shipBehaviour.Decelerate();

            if (IsFiring())
                _shipBehaviour.ShootIfPossible();
        }


        Vector3 GetMousePosition()
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0;
            return mousepos;
        }
        bool IsMenuCalled() => Input.GetKey(KeyCode.Escape);
        bool IsAcelerating() => Input.GetKey(KeyCode.A);
        bool IsDecelerating() => Input.GetKey(KeyCode.D);
        bool IsFiring() => Input.GetMouseButton(0);

    }
}
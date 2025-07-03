using UnityEngine;

namespace FractionGame.Inputs { 
    public class InputManager : MonoBehaviour
    {
        // Manager objects
        [SerializeField] PointerManager pointerManager;

        private InputSystem_Actions inputActions;
        void Awake()
        {
            inputActions = new InputSystem_Actions();
            // Initialize all managers
            pointerManager.Initialize(inputActions.UI.Click);
        }
    }
}



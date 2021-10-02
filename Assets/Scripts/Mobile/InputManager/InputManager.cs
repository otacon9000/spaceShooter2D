using UnityEngine;
using UnityEngine.InputSystem;

#if !UNITY_EDITOR
public class InputManager : MonoBehaviour,  Swipe.IGameplayActions{

    [SerializeField]
    private InputActionAsset m_baseInputs = null;

    Swipe controls;

    public delegate void ChangeDirectionHandler(Vector2 _direction);
    public static event ChangeDirectionHandler OnChangeDirection;

    public void OnEnable()
    {
        if (controls == null)
        {
            controls = new Swipe();
            controls.Gameplay.SetCallbacks(this);
        }
        controls.Gameplay.Enable();
    }

    public void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    public void OnChangePosition(InputAction.CallbackContext context){
        OnChangeDirection(Vector2.zero);
    }
    public void OnSwipeX(InputAction.CallbackContext context){
        Vector2 direction = context.ReadValue<Vector2>();
        OnChangeDirection(direction);
    }
}
#endif
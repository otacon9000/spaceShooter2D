using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class InputManager : MonoBehaviour,  Swipe.IGameplayActions{

    Swipe controls;

    public delegate void ChangeDirectionHandler(float _direction);
    public static event ChangeDirectionHandler OnChangeDirection;

    public void Awake()
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
        if (OnChangeDirection != null)
            OnChangeDirection(0);
    }

    bool enable = true;

    public void OnSwipeX(InputAction.CallbackContext context){
        if (!enable)
            return;
        float direction = context.ReadValue<float>();
        if (direction > 0.1f || direction < -.1f)
            StartCoroutine(SendInput(direction));
    }

    IEnumerator SendInput(float dir)
    {
        enable = false;
        if (OnChangeDirection != null)
            OnChangeDirection(dir);
        yield return new WaitForSeconds(.2f);
        enable = true;
    }

}

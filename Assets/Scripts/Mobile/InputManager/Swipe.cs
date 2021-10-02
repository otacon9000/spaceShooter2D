// GENERATED AUTOMATICALLY FROM 'Assets/Data/Inputs/Swipe.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Swipe : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Swipe()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Swipe"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""6455f4c0-c581-496b-80aa-d87fb8851737"",
            ""actions"": [
                {
                    ""name"": ""SwipeX"",
                    ""type"": ""Value"",
                    ""id"": ""df6b78c5-e8ec-422e-a711-415f89dfe2b2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangePosition"",
                    ""type"": ""Button"",
                    ""id"": ""5ef435de-e39b-43c2-99f2-f42e97e4376e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9ea171ad-f925-4209-b3a2-3d5cef08192e"",
                    ""path"": ""<Touchscreen>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwipeX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4722deb1-461d-4639-92f8-4d976ea3e0e0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_SwipeX = m_Gameplay.FindAction("SwipeX", throwIfNotFound: true);
        m_Gameplay_ChangePosition = m_Gameplay.FindAction("ChangePosition", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_SwipeX;
    private readonly InputAction m_Gameplay_ChangePosition;
    public struct GameplayActions
    {
        private @Swipe m_Wrapper;
        public GameplayActions(@Swipe wrapper) { m_Wrapper = wrapper; }
        public InputAction @SwipeX => m_Wrapper.m_Gameplay_SwipeX;
        public InputAction @ChangePosition => m_Wrapper.m_Gameplay_ChangePosition;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @SwipeX.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwipeX;
                @SwipeX.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwipeX;
                @SwipeX.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwipeX;
                @ChangePosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangePosition;
                @ChangePosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangePosition;
                @ChangePosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangePosition;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SwipeX.started += instance.OnSwipeX;
                @SwipeX.performed += instance.OnSwipeX;
                @SwipeX.canceled += instance.OnSwipeX;
                @ChangePosition.started += instance.OnChangePosition;
                @ChangePosition.performed += instance.OnChangePosition;
                @ChangePosition.canceled += instance.OnChangePosition;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnSwipeX(InputAction.CallbackContext context);
        void OnChangePosition(InputAction.CallbackContext context);
    }
}

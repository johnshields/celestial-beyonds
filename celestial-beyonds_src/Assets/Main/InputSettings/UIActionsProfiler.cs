//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Main/InputSettings/UIActionsProfiler.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @UIActionsProfiler : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @UIActionsProfiler()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIActionsProfiler"",
    ""maps"": [
        {
            ""name"": ""UIActions"",
            ""id"": ""3ef7450c-19f2-4745-99a0-51f86a9e173e"",
            ""actions"": [
                {
                    ""name"": ""StartGame"",
                    ""type"": ""Button"",
                    ""id"": ""66570f26-8390-4f15-9a18-b9ee1f57863e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayCinematic"",
                    ""type"": ""Button"",
                    ""id"": ""ecd5128b-136f-4ad2-8a57-0df31fe99eb9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cc31c827-2db6-413b-bb0c-7f86ea19321b"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83a0e31b-449e-41b8-ae4e-57c754fec5b1"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e83e7531-5374-4e49-af7f-85b7aa99790f"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayCinematic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""255d850b-962e-4860-a6df-f070e0ed22a9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayCinematic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UIActions
        m_UIActions = asset.FindActionMap("UIActions", throwIfNotFound: true);
        m_UIActions_StartGame = m_UIActions.FindAction("StartGame", throwIfNotFound: true);
        m_UIActions_PlayCinematic = m_UIActions.FindAction("PlayCinematic", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // UIActions
    private readonly InputActionMap m_UIActions;
    private IUIActionsActions m_UIActionsActionsCallbackInterface;
    private readonly InputAction m_UIActions_StartGame;
    private readonly InputAction m_UIActions_PlayCinematic;
    public struct UIActionsActions
    {
        private @UIActionsProfiler m_Wrapper;
        public UIActionsActions(@UIActionsProfiler wrapper) { m_Wrapper = wrapper; }
        public InputAction @StartGame => m_Wrapper.m_UIActions_StartGame;
        public InputAction @PlayCinematic => m_Wrapper.m_UIActions_PlayCinematic;
        public InputActionMap Get() { return m_Wrapper.m_UIActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActionsActions set) { return set.Get(); }
        public void SetCallbacks(IUIActionsActions instance)
        {
            if (m_Wrapper.m_UIActionsActionsCallbackInterface != null)
            {
                @StartGame.started -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnStartGame;
                @StartGame.performed -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnStartGame;
                @StartGame.canceled -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnStartGame;
                @PlayCinematic.started -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnPlayCinematic;
                @PlayCinematic.performed -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnPlayCinematic;
                @PlayCinematic.canceled -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnPlayCinematic;
            }
            m_Wrapper.m_UIActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @StartGame.started += instance.OnStartGame;
                @StartGame.performed += instance.OnStartGame;
                @StartGame.canceled += instance.OnStartGame;
                @PlayCinematic.started += instance.OnPlayCinematic;
                @PlayCinematic.performed += instance.OnPlayCinematic;
                @PlayCinematic.canceled += instance.OnPlayCinematic;
            }
        }
    }
    public UIActionsActions @UIActions => new UIActionsActions(this);
    public interface IUIActionsActions
    {
        void OnStartGame(InputAction.CallbackContext context);
        void OnPlayCinematic(InputAction.CallbackContext context);
    }
}
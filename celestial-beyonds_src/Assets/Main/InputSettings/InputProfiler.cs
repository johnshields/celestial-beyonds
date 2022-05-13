// GENERATED AUTOMATICALLY FROM 'Assets/Main/InputSettings/InputProfiler.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputProfiler : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputProfiler()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputProfiler"",
    ""maps"": [
        {
            ""name"": ""Profiler"",
            ""id"": ""f49beba3-b51d-4298-91c1-76778cf0afc4"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""433e7d36-1d4a-4534-ac5f-90e04cb514fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d43905fb-ae74-4e5f-a571-2ef8f9a60faa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""100f2a91-9421-400a-a0f3-644c76d4072c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""dd9e8aa9-7ea8-4488-97ff-862b16384ae1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""805238ac-9544-4d8a-b9d3-dfef456d24ba"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6c7390f3-8675-42a0-a9be-fe20fa49dccd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b284b39f-3b40-47fd-a59c-8737b6ba6b3c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3743fd10-e77b-4872-8f30-3e7ee8c58136"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""881d10fe-0695-4ce1-94e2-40703ea34e7f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""155c5541-78b2-41e8-95da-1021d95e67f9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7afea426-7926-4a3c-a7ee-d46c5f458fdf"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1ee3f5af-acd0-44dd-83a5-55997091482f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4cb723d9-c233-4128-8dba-f58e8fd6577c"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ccf7c3e4-ee99-4290-bf3e-93d60771b6fb"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0448eba7-1b3c-4a14-80dc-c5ae8ee07d61"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a84fe81c-ca6c-40f3-af79-7b1d5eec4a10"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bc11560-f570-4ebe-b5ab-39e9f31b1126"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Profiler
        m_Profiler = asset.FindActionMap("Profiler", throwIfNotFound: true);
        m_Profiler_Move = m_Profiler.FindAction("Move", throwIfNotFound: true);
        m_Profiler_Jump = m_Profiler.FindAction("Jump", throwIfNotFound: true);
        m_Profiler_Attack = m_Profiler.FindAction("Attack", throwIfNotFound: true);
        m_Profiler_Look = m_Profiler.FindAction("Look", throwIfNotFound: true);
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

    // Profiler
    private readonly InputActionMap m_Profiler;
    private IProfilerActions m_ProfilerActionsCallbackInterface;
    private readonly InputAction m_Profiler_Move;
    private readonly InputAction m_Profiler_Jump;
    private readonly InputAction m_Profiler_Attack;
    private readonly InputAction m_Profiler_Look;
    public struct ProfilerActions
    {
        private @InputProfiler m_Wrapper;
        public ProfilerActions(@InputProfiler wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Profiler_Move;
        public InputAction @Jump => m_Wrapper.m_Profiler_Jump;
        public InputAction @Attack => m_Wrapper.m_Profiler_Attack;
        public InputAction @Look => m_Wrapper.m_Profiler_Look;
        public InputActionMap Get() { return m_Wrapper.m_Profiler; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ProfilerActions set) { return set.Get(); }
        public void SetCallbacks(IProfilerActions instance)
        {
            if (m_Wrapper.m_ProfilerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnAttack;
                @Look.started -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_ProfilerActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_ProfilerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public ProfilerActions @Profiler => new ProfilerActions(this);
    public interface IProfilerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/Player/PlayerInput.inputactions
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

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""8aa6152c-155b-4a06-b4c9-180dc367f1be"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""461fe860-4770-4d09-a95f-e7f71527b131"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Running"",
                    ""type"": ""Button"",
                    ""id"": ""fc0f97db-4ca5-4a40-9e4a-d3e573ae3124"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jumping"",
                    ""type"": ""Button"",
                    ""id"": ""82b0026f-81b5-453c-8781-cf5d3530fa9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkipDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""ec1c585c-7dfe-4765-9301-dd5a6049d494"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""1cedef20-aedf-4300-bb1e-8f8936dac70b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2523a2f1-398d-465f-a918-99b774b6be1e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1f883781-c8d2-4a13-86d8-13a69460d57e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d7892033-16ab-4426-a4b7-90bf12a8ef7a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ec5b66e2-2d81-454a-a907-2e6d4c2d4dd2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""77859b19-4b73-4b3f-84d9-a0f1bb3f4a25"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""62262960-36ca-49b4-8939-c6e5cb664c1c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e803fe5c-4645-441d-ad72-121281d93195"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41b4ca73-1b39-4d04-8ffa-3e515cfe8bf4"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Running"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b08ebe4e-831a-4a16-afd5-4cd348bf7fe2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7d83a7c-639d-4a4a-9c93-2b6d7ac21b81"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""OnBattle"",
            ""id"": ""a6f04ed5-fb03-4ea4-98fc-844e5883fe4c"",
            ""actions"": [
                {
                    ""name"": ""SkipDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""9a21fa32-a36e-41cc-b8fb-ca3f0b821cc7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""021c34e1-de19-44c9-90a4-5a277ebc7c96"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ccd9fcf-f9ad-411b-ab81-cb790216a2c5"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Running = m_Player.FindAction("Running", throwIfNotFound: true);
        m_Player_Jumping = m_Player.FindAction("Jumping", throwIfNotFound: true);
        m_Player_SkipDialogue = m_Player.FindAction("SkipDialogue", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        // OnBattle
        m_OnBattle = asset.FindActionMap("OnBattle", throwIfNotFound: true);
        m_OnBattle_SkipDialogue = m_OnBattle.FindAction("SkipDialogue", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Running;
    private readonly InputAction m_Player_Jumping;
    private readonly InputAction m_Player_SkipDialogue;
    private readonly InputAction m_Player_Interact;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Running => m_Wrapper.m_Player_Running;
        public InputAction @Jumping => m_Wrapper.m_Player_Jumping;
        public InputAction @SkipDialogue => m_Wrapper.m_Player_SkipDialogue;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Running.started += instance.OnRunning;
            @Running.performed += instance.OnRunning;
            @Running.canceled += instance.OnRunning;
            @Jumping.started += instance.OnJumping;
            @Jumping.performed += instance.OnJumping;
            @Jumping.canceled += instance.OnJumping;
            @SkipDialogue.started += instance.OnSkipDialogue;
            @SkipDialogue.performed += instance.OnSkipDialogue;
            @SkipDialogue.canceled += instance.OnSkipDialogue;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Running.started -= instance.OnRunning;
            @Running.performed -= instance.OnRunning;
            @Running.canceled -= instance.OnRunning;
            @Jumping.started -= instance.OnJumping;
            @Jumping.performed -= instance.OnJumping;
            @Jumping.canceled -= instance.OnJumping;
            @SkipDialogue.started -= instance.OnSkipDialogue;
            @SkipDialogue.performed -= instance.OnSkipDialogue;
            @SkipDialogue.canceled -= instance.OnSkipDialogue;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // OnBattle
    private readonly InputActionMap m_OnBattle;
    private List<IOnBattleActions> m_OnBattleActionsCallbackInterfaces = new List<IOnBattleActions>();
    private readonly InputAction m_OnBattle_SkipDialogue;
    public struct OnBattleActions
    {
        private @PlayerInput m_Wrapper;
        public OnBattleActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @SkipDialogue => m_Wrapper.m_OnBattle_SkipDialogue;
        public InputActionMap Get() { return m_Wrapper.m_OnBattle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OnBattleActions set) { return set.Get(); }
        public void AddCallbacks(IOnBattleActions instance)
        {
            if (instance == null || m_Wrapper.m_OnBattleActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_OnBattleActionsCallbackInterfaces.Add(instance);
            @SkipDialogue.started += instance.OnSkipDialogue;
            @SkipDialogue.performed += instance.OnSkipDialogue;
            @SkipDialogue.canceled += instance.OnSkipDialogue;
        }

        private void UnregisterCallbacks(IOnBattleActions instance)
        {
            @SkipDialogue.started -= instance.OnSkipDialogue;
            @SkipDialogue.performed -= instance.OnSkipDialogue;
            @SkipDialogue.canceled -= instance.OnSkipDialogue;
        }

        public void RemoveCallbacks(IOnBattleActions instance)
        {
            if (m_Wrapper.m_OnBattleActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IOnBattleActions instance)
        {
            foreach (var item in m_Wrapper.m_OnBattleActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_OnBattleActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public OnBattleActions @OnBattle => new OnBattleActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRunning(InputAction.CallbackContext context);
        void OnJumping(InputAction.CallbackContext context);
        void OnSkipDialogue(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IOnBattleActions
    {
        void OnSkipDialogue(InputAction.CallbackContext context);
    }
}

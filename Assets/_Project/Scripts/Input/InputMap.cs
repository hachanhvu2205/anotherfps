//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/_Project/Data/Input/InputMap.inputactions
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

namespace Source.Input
{
    public partial class @InputMap : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputMap()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""Navigation"",
            ""id"": ""1f7b2e65-4f7c-4aa5-acc1-2dec96cdc1c1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""bc6749a1-5035-48ce-ab39-540ee3c39249"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""96074ab6-e49a-4129-a0e6-9ca6fd940f0e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""18d822fd-fb5e-4862-aa0f-7571acd90c5f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""929c4b87-b182-4d13-80f5-456238b2adea"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3722ca47-6b1b-4d98-bdf8-77f78c40bf6f"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""0f5b0694-3e90-4fff-896d-caaff88581d8"",
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
                    ""id"": ""dc7ff0c4-81be-4a3b-8ad9-c67735ef70fe"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a31f7918-dc68-4660-9e96-0eca3bbd2313"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3a83d8a1-e9f3-49bd-b107-7901685d60c1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""718e463a-e033-453f-9ff3-4ec4a2201f04"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""FreeLook"",
            ""id"": ""26ec75ad-ec40-489f-9cb4-f3d9ce727eb3"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""8fc8ff57-de1c-4c52-9973-28828f972b0a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7ce6c893-740f-484d-9c73-38a101b48a5b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Desktop"",
            ""bindingGroup"": ""Desktop"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Navigation
            m_Navigation = asset.FindActionMap("Navigation", throwIfNotFound: true);
            m_Navigation_Move = m_Navigation.FindAction("Move", throwIfNotFound: true);
            m_Navigation_Jump = m_Navigation.FindAction("Jump", throwIfNotFound: true);
            m_Navigation_Crouch = m_Navigation.FindAction("Crouch", throwIfNotFound: true);
            // FreeLook
            m_FreeLook = asset.FindActionMap("FreeLook", throwIfNotFound: true);
            m_FreeLook_Look = m_FreeLook.FindAction("Look", throwIfNotFound: true);
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

        // Navigation
        private readonly InputActionMap m_Navigation;
        private INavigationActions m_NavigationActionsCallbackInterface;
        private readonly InputAction m_Navigation_Move;
        private readonly InputAction m_Navigation_Jump;
        private readonly InputAction m_Navigation_Crouch;
        public struct NavigationActions
        {
            private @InputMap m_Wrapper;
            public NavigationActions(@InputMap wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Navigation_Move;
            public InputAction @Jump => m_Wrapper.m_Navigation_Jump;
            public InputAction @Crouch => m_Wrapper.m_Navigation_Crouch;
            public InputActionMap Get() { return m_Wrapper.m_Navigation; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(NavigationActions set) { return set.Get(); }
            public void SetCallbacks(INavigationActions instance)
            {
                if (m_Wrapper.m_NavigationActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_NavigationActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_NavigationActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_NavigationActionsCallbackInterface.OnMove;
                    @Jump.started -= m_Wrapper.m_NavigationActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_NavigationActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_NavigationActionsCallbackInterface.OnJump;
                    @Crouch.started -= m_Wrapper.m_NavigationActionsCallbackInterface.OnCrouch;
                    @Crouch.performed -= m_Wrapper.m_NavigationActionsCallbackInterface.OnCrouch;
                    @Crouch.canceled -= m_Wrapper.m_NavigationActionsCallbackInterface.OnCrouch;
                }
                m_Wrapper.m_NavigationActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Crouch.started += instance.OnCrouch;
                    @Crouch.performed += instance.OnCrouch;
                    @Crouch.canceled += instance.OnCrouch;
                }
            }
        }
        public NavigationActions @Navigation => new NavigationActions(this);

        // FreeLook
        private readonly InputActionMap m_FreeLook;
        private IFreeLookActions m_FreeLookActionsCallbackInterface;
        private readonly InputAction m_FreeLook_Look;
        public struct FreeLookActions
        {
            private @InputMap m_Wrapper;
            public FreeLookActions(@InputMap wrapper) { m_Wrapper = wrapper; }
            public InputAction @Look => m_Wrapper.m_FreeLook_Look;
            public InputActionMap Get() { return m_Wrapper.m_FreeLook; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(FreeLookActions set) { return set.Get(); }
            public void SetCallbacks(IFreeLookActions instance)
            {
                if (m_Wrapper.m_FreeLookActionsCallbackInterface != null)
                {
                    @Look.started -= m_Wrapper.m_FreeLookActionsCallbackInterface.OnLook;
                    @Look.performed -= m_Wrapper.m_FreeLookActionsCallbackInterface.OnLook;
                    @Look.canceled -= m_Wrapper.m_FreeLookActionsCallbackInterface.OnLook;
                }
                m_Wrapper.m_FreeLookActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                }
            }
        }
        public FreeLookActions @FreeLook => new FreeLookActions(this);
        private int m_DesktopSchemeIndex = -1;
        public InputControlScheme DesktopScheme
        {
            get
            {
                if (m_DesktopSchemeIndex == -1) m_DesktopSchemeIndex = asset.FindControlSchemeIndex("Desktop");
                return asset.controlSchemes[m_DesktopSchemeIndex];
            }
        }
        public interface INavigationActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnCrouch(InputAction.CallbackContext context);
        }
        public interface IFreeLookActions
        {
            void OnLook(InputAction.CallbackContext context);
        }
    }
}
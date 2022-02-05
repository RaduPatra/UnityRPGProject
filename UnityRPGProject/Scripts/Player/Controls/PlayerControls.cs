// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/Controls/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player Movement"",
            ""id"": ""623377ee-b01b-4fd2-85e1-79b8778bdeda"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9994268a-d9ec-48d0-ad5f-bb1e7cf4bb91"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""8084a81b-5479-47d1-87c0-b1eb279bac54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ecd131fa-8901-4ae4-a338-83118e435b52"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c4f9d71b-0f77-4dae-9129-c9b15465301c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""91ef7bfd-5b47-4ac6-89ca-7685cb5c4277"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestButton"",
                    ""type"": ""Button"",
                    ""id"": ""6277a552-8b48-40e1-a3ef-de233e8db93a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseTest"",
                    ""type"": ""Button"",
                    ""id"": ""55d13b3e-3b7e-4870-89ec-2412ec164692"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""2a3f9538-fad3-4ba9-b6f2-336d05bffdef"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""360522e6-9cfe-4652-8580-92d15beed889"",
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
                    ""id"": ""95985edc-2f21-41be-ad42-d80170314b0d"",
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
                    ""id"": ""ecc778a7-e6e9-4387-9847-ccc8ba4aae22"",
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
                    ""id"": ""ef620573-27fe-4384-9228-8b6799c56399"",
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
                    ""id"": ""740c4f93-9931-4e6f-b2c0-7e932fa65e6d"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e086ead1-a6d3-438f-8029-8846734324af"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bae6cb5b-d680-42a4-a6a1-7e85a653b1b8"",
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
                    ""id"": ""77dda7ad-49d6-4445-bfb0-8d0aecb617f9"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""48424258-f32f-42b1-8b36-cc109ddc9806"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestButton"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""be1b6efa-042f-4cf2-aeeb-984698ad486d"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""84cb3c1d-4a73-48d8-82e8-3e3663d5800d"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a18db508-71d6-4a54-8766-e17d15d57a2e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player Attack"",
            ""id"": ""89a779dd-d568-4f25-8619-2bd111366407"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""9070562c-4fdf-42e2-9ece-5563cddf9c30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hotbar"",
                    ""type"": ""Value"",
                    ""id"": ""a21fbfea-5bbd-4488-a7ee-ed8cf1fd9d3e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bd28b808-d988-4a22-880a-e50e8970662d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""890722fb-61e4-4a0f-9fa6-f068be61d129"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": ""Hotbar"",
                    ""groups"": """",
                    ""action"": ""Hotbar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf18f534-c157-43e1-b5d7-6e0e98ca076d"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": ""Hotbar(valueShift=1)"",
                    ""groups"": """",
                    ""action"": ""Hotbar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""935c80eb-2dfc-4e41-93a2-cc58408e8969"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": ""Hotbar(valueShift=2)"",
                    ""groups"": """",
                    ""action"": ""Hotbar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22736c3f-f1cf-42ef-8d29-90ea7639eb9a"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": ""Hotbar(valueShift=3)"",
                    ""groups"": """",
                    ""action"": ""Hotbar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2d01d7b-fe8a-4b51-8f37-11e14dabdb57"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": ""Hotbar(valueShift=4)"",
                    ""groups"": """",
                    ""action"": ""Hotbar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MouseTest"",
            ""id"": ""3fa43944-b329-495f-90f9-a9fd7d167b95"",
            ""actions"": [
                {
                    ""name"": ""MouseLookTest"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fc001d9c-f147-437c-9cf4-ffe996d1823b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7341f2f8-62c8-4cb4-a0c3-46ccaf13268a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLookTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Movement
        m_PlayerMovement = asset.FindActionMap("Player Movement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Interact = m_PlayerMovement.FindAction("Interact", throwIfNotFound: true);
        m_PlayerMovement_MouseLook = m_PlayerMovement.FindAction("MouseLook", throwIfNotFound: true);
        m_PlayerMovement_Jump = m_PlayerMovement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMovement_Sprint = m_PlayerMovement.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerMovement_TestButton = m_PlayerMovement.FindAction("TestButton", throwIfNotFound: true);
        m_PlayerMovement_MouseTest = m_PlayerMovement.FindAction("MouseTest", throwIfNotFound: true);
        // Player Attack
        m_PlayerAttack = asset.FindActionMap("Player Attack", throwIfNotFound: true);
        m_PlayerAttack_Attack = m_PlayerAttack.FindAction("Attack", throwIfNotFound: true);
        m_PlayerAttack_Hotbar = m_PlayerAttack.FindAction("Hotbar", throwIfNotFound: true);
        // MouseTest
        m_MouseTest = asset.FindActionMap("MouseTest", throwIfNotFound: true);
        m_MouseTest_MouseLookTest = m_MouseTest.FindAction("MouseLookTest", throwIfNotFound: true);
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

    // Player Movement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Interact;
    private readonly InputAction m_PlayerMovement_MouseLook;
    private readonly InputAction m_PlayerMovement_Jump;
    private readonly InputAction m_PlayerMovement_Sprint;
    private readonly InputAction m_PlayerMovement_TestButton;
    private readonly InputAction m_PlayerMovement_MouseTest;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Interact => m_Wrapper.m_PlayerMovement_Interact;
        public InputAction @MouseLook => m_Wrapper.m_PlayerMovement_MouseLook;
        public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
        public InputAction @Sprint => m_Wrapper.m_PlayerMovement_Sprint;
        public InputAction @TestButton => m_Wrapper.m_PlayerMovement_TestButton;
        public InputAction @MouseTest => m_Wrapper.m_PlayerMovement_MouseTest;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnInteract;
                @MouseLook.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseLook;
                @Jump.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprint;
                @TestButton.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnTestButton;
                @TestButton.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnTestButton;
                @TestButton.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnTestButton;
                @MouseTest.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseTest;
                @MouseTest.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseTest;
                @MouseTest.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseTest;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @TestButton.started += instance.OnTestButton;
                @TestButton.performed += instance.OnTestButton;
                @TestButton.canceled += instance.OnTestButton;
                @MouseTest.started += instance.OnMouseTest;
                @MouseTest.performed += instance.OnMouseTest;
                @MouseTest.canceled += instance.OnMouseTest;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // Player Attack
    private readonly InputActionMap m_PlayerAttack;
    private IPlayerAttackActions m_PlayerAttackActionsCallbackInterface;
    private readonly InputAction m_PlayerAttack_Attack;
    private readonly InputAction m_PlayerAttack_Hotbar;
    public struct PlayerAttackActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerAttackActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_PlayerAttack_Attack;
        public InputAction @Hotbar => m_Wrapper.m_PlayerAttack_Hotbar;
        public InputActionMap Get() { return m_Wrapper.m_PlayerAttack; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerAttackActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerAttackActions instance)
        {
            if (m_Wrapper.m_PlayerAttackActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_PlayerAttackActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerAttackActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerAttackActionsCallbackInterface.OnAttack;
                @Hotbar.started -= m_Wrapper.m_PlayerAttackActionsCallbackInterface.OnHotbar;
                @Hotbar.performed -= m_Wrapper.m_PlayerAttackActionsCallbackInterface.OnHotbar;
                @Hotbar.canceled -= m_Wrapper.m_PlayerAttackActionsCallbackInterface.OnHotbar;
            }
            m_Wrapper.m_PlayerAttackActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Hotbar.started += instance.OnHotbar;
                @Hotbar.performed += instance.OnHotbar;
                @Hotbar.canceled += instance.OnHotbar;
            }
        }
    }
    public PlayerAttackActions @PlayerAttack => new PlayerAttackActions(this);

    // MouseTest
    private readonly InputActionMap m_MouseTest;
    private IMouseTestActions m_MouseTestActionsCallbackInterface;
    private readonly InputAction m_MouseTest_MouseLookTest;
    public struct MouseTestActions
    {
        private @PlayerControls m_Wrapper;
        public MouseTestActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLookTest => m_Wrapper.m_MouseTest_MouseLookTest;
        public InputActionMap Get() { return m_Wrapper.m_MouseTest; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseTestActions set) { return set.Get(); }
        public void SetCallbacks(IMouseTestActions instance)
        {
            if (m_Wrapper.m_MouseTestActionsCallbackInterface != null)
            {
                @MouseLookTest.started -= m_Wrapper.m_MouseTestActionsCallbackInterface.OnMouseLookTest;
                @MouseLookTest.performed -= m_Wrapper.m_MouseTestActionsCallbackInterface.OnMouseLookTest;
                @MouseLookTest.canceled -= m_Wrapper.m_MouseTestActionsCallbackInterface.OnMouseLookTest;
            }
            m_Wrapper.m_MouseTestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseLookTest.started += instance.OnMouseLookTest;
                @MouseLookTest.performed += instance.OnMouseLookTest;
                @MouseLookTest.canceled += instance.OnMouseLookTest;
            }
        }
    }
    public MouseTestActions @MouseTest => new MouseTestActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnTestButton(InputAction.CallbackContext context);
        void OnMouseTest(InputAction.CallbackContext context);
    }
    public interface IPlayerAttackActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnHotbar(InputAction.CallbackContext context);
    }
    public interface IMouseTestActions
    {
        void OnMouseLookTest(InputAction.CallbackContext context);
    }
}

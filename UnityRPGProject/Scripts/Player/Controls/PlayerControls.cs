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
            ""name"": ""Gameplay"",
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
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""7d9a22f6-0bf5-4e69-afe2-624ea288190f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpecialAttack"",
                    ""type"": ""Button"",
                    ""id"": ""911b26c5-788f-44f6-92e5-634da40f8399"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""b6df7d53-64f2-478d-85eb-d83523aa6f1f"",
                    ""expectedControlType"": ""Button"",
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
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""050e8a2b-3d7c-47af-92bd-dfa528c6702c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""334086f5-fa65-4cbf-89e0-85cf6a03ebb2"",
                    ""expectedControlType"": ""Vector2"",
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
                    ""name"": """",
                    ""id"": ""bce09997-9a4f-47c0-a7f7-8fb61c398eb1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Hold(duration=0.4,pressPoint=0.2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48ca5262-6e36-4d08-92af-f9f37a43102d"",
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
                    ""id"": ""9f437224-7532-4d71-80ea-a7d7666942bc"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ea3422a-855a-497f-8983-a88858c40698"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3f06999-6d91-458d-9653-9df9878ecb49"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
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
            ""name"": ""General"",
            ""id"": ""3fa43944-b329-495f-90f9-a9fd7d167b95"",
            ""actions"": [
                {
                    ""name"": ""Hotbar"",
                    ""type"": ""Value"",
                    ""id"": ""746b4917-9388-402a-8b56-a6b20f82224d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleUI"",
                    ""type"": ""Button"",
                    ""id"": ""2342be97-14a5-4d9c-982a-bd3ca26a607b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""51e32627-6024-4a63-8e25-6af55d34d9ff"",
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
                    ""id"": ""40e74706-3471-41f4-b7ea-0f77219235ce"",
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
                    ""id"": ""89fa4e2d-afb5-43ea-9328-9a266366371c"",
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
                    ""id"": ""0289db88-1bd2-4268-bf0a-8a5b4db2d224"",
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
                    ""id"": ""564763ed-5afe-4628-9107-0679d246adff"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": ""Hotbar(valueShift=4)"",
                    ""groups"": """",
                    ""action"": ""Hotbar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2465b9cb-ff01-45d7-ac91-e982a0e3f046"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interaction"",
            ""id"": ""160c7e48-6340-4136-b897-52b7c70ce24e"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""801ceb33-9cbb-45a7-a92c-b2d45f8c76a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""3509b9f0-be69-4aa3-bdba-9fd4434f7b26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""11b7cd7c-ccb5-408c-b722-42ff43ead2e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b375138b-414e-4c75-be63-cb53774d80e4"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5031f74a-39dd-49a6-a4b9-7d4f53191d80"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""375f6795-11ad-4aa0-8760-0ed11c4fe43e"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Testing"",
            ""id"": ""464839a6-f97f-495e-95af-59868e441d8c"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""dee3f529-f0dc-4c72-84d5-9c465312a378"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestButton"",
                    ""type"": ""Button"",
                    ""id"": ""01fa09f5-f42e-4fa1-91c5-d1b3c24e64c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseTest"",
                    ""type"": ""Button"",
                    ""id"": ""d67cb070-3d35-431a-8b9c-5acbd45f02f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""48ace088-db0a-4e1a-806f-184f1a683d87"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""37d0e336-b1d4-4295-bb62-8abeee4696db"",
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
                    ""id"": ""050f9d37-f3ba-43c7-9273-16901d6b2bf7"",
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
                    ""id"": ""7eb769d0-0242-435c-9d52-1349b9a5d388"",
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
                    ""id"": ""a7331161-04c8-4570-af0a-4d8a86ba528c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseTest"",
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
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_Attack = m_Gameplay.FindAction("Attack", throwIfNotFound: true);
        m_Gameplay_SpecialAttack = m_Gameplay.FindAction("SpecialAttack", throwIfNotFound: true);
        m_Gameplay_Block = m_Gameplay.FindAction("Block", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Sprint = m_Gameplay.FindAction("Sprint", throwIfNotFound: true);
        m_Gameplay_Roll = m_Gameplay.FindAction("Roll", throwIfNotFound: true);
        m_Gameplay_MouseLook = m_Gameplay.FindAction("MouseLook", throwIfNotFound: true);
        // Player Attack
        m_PlayerAttack = asset.FindActionMap("Player Attack", throwIfNotFound: true);
        m_PlayerAttack_Attack = m_PlayerAttack.FindAction("Attack", throwIfNotFound: true);
        m_PlayerAttack_Hotbar = m_PlayerAttack.FindAction("Hotbar", throwIfNotFound: true);
        // General
        m_General = asset.FindActionMap("General", throwIfNotFound: true);
        m_General_Hotbar = m_General.FindAction("Hotbar", throwIfNotFound: true);
        m_General_ToggleUI = m_General.FindAction("ToggleUI", throwIfNotFound: true);
        // Interaction
        m_Interaction = asset.FindActionMap("Interaction", throwIfNotFound: true);
        m_Interaction_Newaction = m_Interaction.FindAction("New action", throwIfNotFound: true);
        m_Interaction_Interact = m_Interaction.FindAction("Interact", throwIfNotFound: true);
        m_Interaction_Cancel = m_Interaction.FindAction("Cancel", throwIfNotFound: true);
        // Testing
        m_Testing = asset.FindActionMap("Testing", throwIfNotFound: true);
        m_Testing_Newaction = m_Testing.FindAction("New action", throwIfNotFound: true);
        m_Testing_TestButton = m_Testing.FindAction("TestButton", throwIfNotFound: true);
        m_Testing_MouseTest = m_Testing.FindAction("MouseTest", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_Attack;
    private readonly InputAction m_Gameplay_SpecialAttack;
    private readonly InputAction m_Gameplay_Block;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Sprint;
    private readonly InputAction m_Gameplay_Roll;
    private readonly InputAction m_Gameplay_MouseLook;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @Attack => m_Wrapper.m_Gameplay_Attack;
        public InputAction @SpecialAttack => m_Wrapper.m_Gameplay_SpecialAttack;
        public InputAction @Block => m_Wrapper.m_Gameplay_Block;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Sprint => m_Wrapper.m_Gameplay_Sprint;
        public InputAction @Roll => m_Wrapper.m_Gameplay_Roll;
        public InputAction @MouseLook => m_Wrapper.m_Gameplay_MouseLook;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Attack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @SpecialAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAttack;
                @Block.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Roll.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @MouseLook.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLook;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @SpecialAttack.started += instance.OnSpecialAttack;
                @SpecialAttack.performed += instance.OnSpecialAttack;
                @SpecialAttack.canceled += instance.OnSpecialAttack;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

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

    // General
    private readonly InputActionMap m_General;
    private IGeneralActions m_GeneralActionsCallbackInterface;
    private readonly InputAction m_General_Hotbar;
    private readonly InputAction m_General_ToggleUI;
    public struct GeneralActions
    {
        private @PlayerControls m_Wrapper;
        public GeneralActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Hotbar => m_Wrapper.m_General_Hotbar;
        public InputAction @ToggleUI => m_Wrapper.m_General_ToggleUI;
        public InputActionMap Get() { return m_Wrapper.m_General; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralActions instance)
        {
            if (m_Wrapper.m_GeneralActionsCallbackInterface != null)
            {
                @Hotbar.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnHotbar;
                @Hotbar.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnHotbar;
                @Hotbar.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnHotbar;
                @ToggleUI.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnToggleUI;
                @ToggleUI.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnToggleUI;
                @ToggleUI.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnToggleUI;
            }
            m_Wrapper.m_GeneralActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Hotbar.started += instance.OnHotbar;
                @Hotbar.performed += instance.OnHotbar;
                @Hotbar.canceled += instance.OnHotbar;
                @ToggleUI.started += instance.OnToggleUI;
                @ToggleUI.performed += instance.OnToggleUI;
                @ToggleUI.canceled += instance.OnToggleUI;
            }
        }
    }
    public GeneralActions @General => new GeneralActions(this);

    // Interaction
    private readonly InputActionMap m_Interaction;
    private IInteractionActions m_InteractionActionsCallbackInterface;
    private readonly InputAction m_Interaction_Newaction;
    private readonly InputAction m_Interaction_Interact;
    private readonly InputAction m_Interaction_Cancel;
    public struct InteractionActions
    {
        private @PlayerControls m_Wrapper;
        public InteractionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Interaction_Newaction;
        public InputAction @Interact => m_Wrapper.m_Interaction_Interact;
        public InputAction @Cancel => m_Wrapper.m_Interaction_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_Interaction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionActions set) { return set.Get(); }
        public void SetCallbacks(IInteractionActions instance)
        {
            if (m_Wrapper.m_InteractionActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnNewaction;
                @Interact.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @Cancel.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_InteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public InteractionActions @Interaction => new InteractionActions(this);

    // Testing
    private readonly InputActionMap m_Testing;
    private ITestingActions m_TestingActionsCallbackInterface;
    private readonly InputAction m_Testing_Newaction;
    private readonly InputAction m_Testing_TestButton;
    private readonly InputAction m_Testing_MouseTest;
    public struct TestingActions
    {
        private @PlayerControls m_Wrapper;
        public TestingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Testing_Newaction;
        public InputAction @TestButton => m_Wrapper.m_Testing_TestButton;
        public InputAction @MouseTest => m_Wrapper.m_Testing_MouseTest;
        public InputActionMap Get() { return m_Wrapper.m_Testing; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestingActions set) { return set.Get(); }
        public void SetCallbacks(ITestingActions instance)
        {
            if (m_Wrapper.m_TestingActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_TestingActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_TestingActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_TestingActionsCallbackInterface.OnNewaction;
                @TestButton.started -= m_Wrapper.m_TestingActionsCallbackInterface.OnTestButton;
                @TestButton.performed -= m_Wrapper.m_TestingActionsCallbackInterface.OnTestButton;
                @TestButton.canceled -= m_Wrapper.m_TestingActionsCallbackInterface.OnTestButton;
                @MouseTest.started -= m_Wrapper.m_TestingActionsCallbackInterface.OnMouseTest;
                @MouseTest.performed -= m_Wrapper.m_TestingActionsCallbackInterface.OnMouseTest;
                @MouseTest.canceled -= m_Wrapper.m_TestingActionsCallbackInterface.OnMouseTest;
            }
            m_Wrapper.m_TestingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
                @TestButton.started += instance.OnTestButton;
                @TestButton.performed += instance.OnTestButton;
                @TestButton.canceled += instance.OnTestButton;
                @MouseTest.started += instance.OnMouseTest;
                @MouseTest.performed += instance.OnMouseTest;
                @MouseTest.canceled += instance.OnMouseTest;
            }
        }
    }
    public TestingActions @Testing => new TestingActions(this);
    public interface IGameplayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnSpecialAttack(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
    }
    public interface IPlayerAttackActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnHotbar(InputAction.CallbackContext context);
    }
    public interface IGeneralActions
    {
        void OnHotbar(InputAction.CallbackContext context);
        void OnToggleUI(InputAction.CallbackContext context);
    }
    public interface IInteractionActions
    {
        void OnNewaction(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface ITestingActions
    {
        void OnNewaction(InputAction.CallbackContext context);
        void OnTestButton(InputAction.CallbackContext context);
        void OnMouseTest(InputAction.CallbackContext context);
    }
}

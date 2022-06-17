//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputSetup/PlayerInput.inputactions
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

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""ExplorationMode"",
            ""id"": ""8d5c92b1-26be-425e-b2f4-8f72d51ff4e8"",
            ""actions"": [
                {
                    ""name"": ""MovementInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""754f26f1-9ffc-4851-8291-bd63aba5938b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""efe25bb0-eac4-453d-b2b8-cb9379b445ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c32410e4-9223-4a68-b3f4-e908c331e647"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Primary"",
                    ""type"": ""Button"",
                    ""id"": ""43195818-cdd1-43b7-bfc3-90ca0cbee96d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""2cc2c38e-ba8d-4168-8407-38eb4c8d2d57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""09bc4521-b973-4310-ac8b-43af375e0ddf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability1"",
                    ""type"": ""Button"",
                    ""id"": ""4e192389-beb6-4700-8720-6b99e6dde857"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability2"",
                    ""type"": ""Button"",
                    ""id"": ""006765a9-63ad-4181-8215-f359a8a26565"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpecialAbility"",
                    ""type"": ""Button"",
                    ""id"": ""66253118-6f3f-44eb-b80c-fbfb47176bf7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""85bd7eb9-2773-4685-aa29-005822382e46"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""34e92d44-9322-44ae-8034-031a088a07c8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MovementInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d828d44a-c9b7-4b60-83e8-ec860b54c406"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MovementInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9d02c87b-5c44-42b8-86aa-49d43dd2be21"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MovementInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""798aa710-0981-4401-8eca-81c82cae76aa"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MovementInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""641505e6-829f-4ffe-b0bd-cc54eb4d1bed"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MovementInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a15be65-6ae0-4f64-bab9-aaae4d304976"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""000dfcbc-ee82-4d13-bcad-c6ab1518736b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""183d5b09-12db-4bef-98f5-f1a8f72abec1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""736c0188-2dbd-4795-a2a3-56196b12bae2"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""060d550b-8b01-496f-ad5e-8b28a80f1545"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5883f0d-190d-453e-9b93-8859ffb96f8f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1add34c4-8ee7-45bf-a66b-dd5cb2482294"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11f5e89e-4407-4efe-abc0-88bd6d5fb4d7"",
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
                    ""id"": ""1021898c-768b-4ab3-927f-5cd0251799cd"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""554c5eed-6f94-4d4e-aa25-478585b4a6c2"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a83c37a8-c3c5-4319-a359-d0512d0d4750"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""507b1b0f-fa0f-412c-b71b-60921fa87805"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""SpecialAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""BuildMode"",
            ""id"": ""02e83f62-d882-42b9-8d2b-2b190165b5a3"",
            ""actions"": [
                {
                    ""name"": ""MovementInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5fd526ba-61d6-4da1-b4b6-960aa7f203ea"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bc36b9b1-e2bd-4e7b-b9c4-ae096465e2f0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""bfa3fe6d-8476-4457-bfa1-1fab01f4eb2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Primary"",
                    ""type"": ""Button"",
                    ""id"": ""86140643-8db8-4656-ad82-8e3e05465a31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""a1935b03-01da-4b01-a2af-0b648dec4881"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fcc53d6a-03d0-47b7-9e05-ec8f6489afa0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MovementInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dd37daeb-e459-4a96-95df-5ff7951eeb03"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MovementInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""43babf05-42cc-408a-9a2e-125ab2e0c675"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MovementInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e36fd7fe-157d-42d7-a64c-13adcc9262da"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MovementInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e0750e74-199d-4378-88ec-901ec2111424"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MovementInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f7adf5b-f5fe-40f5-a346-c34af6692da8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53fc0c5b-d730-4f71-bb2f-b62550aa3f50"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""478b4a20-dafb-4bd5-aca6-428f92389190"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""810ac8df-ab3b-40ae-bf05-65b5de94121e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19961cea-c47e-4e99-9fbe-75145866a2b7"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // ExplorationMode
        m_ExplorationMode = asset.FindActionMap("ExplorationMode", throwIfNotFound: true);
        m_ExplorationMode_MovementInput = m_ExplorationMode.FindAction("MovementInput", throwIfNotFound: true);
        m_ExplorationMode_Interact = m_ExplorationMode.FindAction("Interact", throwIfNotFound: true);
        m_ExplorationMode_Jump = m_ExplorationMode.FindAction("Jump", throwIfNotFound: true);
        m_ExplorationMode_Primary = m_ExplorationMode.FindAction("Primary", throwIfNotFound: true);
        m_ExplorationMode_Secondary = m_ExplorationMode.FindAction("Secondary", throwIfNotFound: true);
        m_ExplorationMode_MousePosition = m_ExplorationMode.FindAction("MousePosition", throwIfNotFound: true);
        m_ExplorationMode_Ability1 = m_ExplorationMode.FindAction("Ability1", throwIfNotFound: true);
        m_ExplorationMode_Ability2 = m_ExplorationMode.FindAction("Ability2", throwIfNotFound: true);
        m_ExplorationMode_SpecialAbility = m_ExplorationMode.FindAction("SpecialAbility", throwIfNotFound: true);
        // BuildMode
        m_BuildMode = asset.FindActionMap("BuildMode", throwIfNotFound: true);
        m_BuildMode_MovementInput = m_BuildMode.FindAction("MovementInput", throwIfNotFound: true);
        m_BuildMode_MousePosition = m_BuildMode.FindAction("MousePosition", throwIfNotFound: true);
        m_BuildMode_Secondary = m_BuildMode.FindAction("Secondary", throwIfNotFound: true);
        m_BuildMode_Primary = m_BuildMode.FindAction("Primary", throwIfNotFound: true);
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

    // ExplorationMode
    private readonly InputActionMap m_ExplorationMode;
    private IExplorationModeActions m_ExplorationModeActionsCallbackInterface;
    private readonly InputAction m_ExplorationMode_MovementInput;
    private readonly InputAction m_ExplorationMode_Interact;
    private readonly InputAction m_ExplorationMode_Jump;
    private readonly InputAction m_ExplorationMode_Primary;
    private readonly InputAction m_ExplorationMode_Secondary;
    private readonly InputAction m_ExplorationMode_MousePosition;
    private readonly InputAction m_ExplorationMode_Ability1;
    private readonly InputAction m_ExplorationMode_Ability2;
    private readonly InputAction m_ExplorationMode_SpecialAbility;
    public struct ExplorationModeActions
    {
        private @PlayerInput m_Wrapper;
        public ExplorationModeActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementInput => m_Wrapper.m_ExplorationMode_MovementInput;
        public InputAction @Interact => m_Wrapper.m_ExplorationMode_Interact;
        public InputAction @Jump => m_Wrapper.m_ExplorationMode_Jump;
        public InputAction @Primary => m_Wrapper.m_ExplorationMode_Primary;
        public InputAction @Secondary => m_Wrapper.m_ExplorationMode_Secondary;
        public InputAction @MousePosition => m_Wrapper.m_ExplorationMode_MousePosition;
        public InputAction @Ability1 => m_Wrapper.m_ExplorationMode_Ability1;
        public InputAction @Ability2 => m_Wrapper.m_ExplorationMode_Ability2;
        public InputAction @SpecialAbility => m_Wrapper.m_ExplorationMode_SpecialAbility;
        public InputActionMap Get() { return m_Wrapper.m_ExplorationMode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ExplorationModeActions set) { return set.Get(); }
        public void SetCallbacks(IExplorationModeActions instance)
        {
            if (m_Wrapper.m_ExplorationModeActionsCallbackInterface != null)
            {
                @MovementInput.started -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnMovementInput;
                @MovementInput.performed -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnMovementInput;
                @MovementInput.canceled -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnMovementInput;
                @Interact.started -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnInteract;
                @Jump.started -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnJump;
                @Primary.started -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnPrimary;
                @Primary.performed -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnPrimary;
                @Primary.canceled -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnPrimary;
                @Secondary.started -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnSecondary;
                @Secondary.performed -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnSecondary;
                @Secondary.canceled -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnSecondary;
                @MousePosition.started -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnMousePosition;
                @Ability1.started -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnAbility1;
                @Ability1.performed -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnAbility1;
                @Ability1.canceled -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnAbility1;
                @Ability2.started -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnAbility2;
                @Ability2.performed -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnAbility2;
                @Ability2.canceled -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnAbility2;
                @SpecialAbility.started -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnSpecialAbility;
                @SpecialAbility.performed -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnSpecialAbility;
                @SpecialAbility.canceled -= m_Wrapper.m_ExplorationModeActionsCallbackInterface.OnSpecialAbility;
            }
            m_Wrapper.m_ExplorationModeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovementInput.started += instance.OnMovementInput;
                @MovementInput.performed += instance.OnMovementInput;
                @MovementInput.canceled += instance.OnMovementInput;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Primary.started += instance.OnPrimary;
                @Primary.performed += instance.OnPrimary;
                @Primary.canceled += instance.OnPrimary;
                @Secondary.started += instance.OnSecondary;
                @Secondary.performed += instance.OnSecondary;
                @Secondary.canceled += instance.OnSecondary;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Ability1.started += instance.OnAbility1;
                @Ability1.performed += instance.OnAbility1;
                @Ability1.canceled += instance.OnAbility1;
                @Ability2.started += instance.OnAbility2;
                @Ability2.performed += instance.OnAbility2;
                @Ability2.canceled += instance.OnAbility2;
                @SpecialAbility.started += instance.OnSpecialAbility;
                @SpecialAbility.performed += instance.OnSpecialAbility;
                @SpecialAbility.canceled += instance.OnSpecialAbility;
            }
        }
    }
    public ExplorationModeActions @ExplorationMode => new ExplorationModeActions(this);

    // BuildMode
    private readonly InputActionMap m_BuildMode;
    private IBuildModeActions m_BuildModeActionsCallbackInterface;
    private readonly InputAction m_BuildMode_MovementInput;
    private readonly InputAction m_BuildMode_MousePosition;
    private readonly InputAction m_BuildMode_Secondary;
    private readonly InputAction m_BuildMode_Primary;
    public struct BuildModeActions
    {
        private @PlayerInput m_Wrapper;
        public BuildModeActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementInput => m_Wrapper.m_BuildMode_MovementInput;
        public InputAction @MousePosition => m_Wrapper.m_BuildMode_MousePosition;
        public InputAction @Secondary => m_Wrapper.m_BuildMode_Secondary;
        public InputAction @Primary => m_Wrapper.m_BuildMode_Primary;
        public InputActionMap Get() { return m_Wrapper.m_BuildMode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildModeActions set) { return set.Get(); }
        public void SetCallbacks(IBuildModeActions instance)
        {
            if (m_Wrapper.m_BuildModeActionsCallbackInterface != null)
            {
                @MovementInput.started -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnMovementInput;
                @MovementInput.performed -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnMovementInput;
                @MovementInput.canceled -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnMovementInput;
                @MousePosition.started -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnMousePosition;
                @Secondary.started -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnSecondary;
                @Secondary.performed -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnSecondary;
                @Secondary.canceled -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnSecondary;
                @Primary.started -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnPrimary;
                @Primary.performed -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnPrimary;
                @Primary.canceled -= m_Wrapper.m_BuildModeActionsCallbackInterface.OnPrimary;
            }
            m_Wrapper.m_BuildModeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovementInput.started += instance.OnMovementInput;
                @MovementInput.performed += instance.OnMovementInput;
                @MovementInput.canceled += instance.OnMovementInput;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Secondary.started += instance.OnSecondary;
                @Secondary.performed += instance.OnSecondary;
                @Secondary.canceled += instance.OnSecondary;
                @Primary.started += instance.OnPrimary;
                @Primary.performed += instance.OnPrimary;
                @Primary.canceled += instance.OnPrimary;
            }
        }
    }
    public BuildModeActions @BuildMode => new BuildModeActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IExplorationModeActions
    {
        void OnMovementInput(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPrimary(InputAction.CallbackContext context);
        void OnSecondary(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnAbility1(InputAction.CallbackContext context);
        void OnAbility2(InputAction.CallbackContext context);
        void OnSpecialAbility(InputAction.CallbackContext context);
    }
    public interface IBuildModeActions
    {
        void OnMovementInput(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnSecondary(InputAction.CallbackContext context);
        void OnPrimary(InputAction.CallbackContext context);
    }
}

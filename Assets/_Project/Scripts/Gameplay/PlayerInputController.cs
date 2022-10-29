/**
 * PlayerInputController.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2022-10-29
 */

using Source.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Gameplay
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField]
        InputActionReference _freeLook;
        [SerializeField]
        InputActionReference _moveAction;

        CharacterMovement _movement;

        void Awake()
        {
            _movement = GetComponent<CharacterMovement>();
        }

        void OnEnable()
        {
            _freeLook.action.Enable();
            _moveAction.action.Enable();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void OnDisable()
        {
            _freeLook.action.Disable();
            _moveAction.action.Disable();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        void Update()
        {
            _movement.Move(_moveAction.action.ReadValue<Vector2>());
        }
    }
}
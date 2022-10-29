/**
 * PlayerInputController.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2022-10-29
 */

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
        CameraController _camera;

        void Awake()
        {
            _movement = GetComponent<CharacterMovement>();
            _camera = GetComponent<CameraController>();
        }

        void OnEnable()
        {
            _freeLook.action.Enable();
            _moveAction.action.Enable();
        }

        void OnDisable()
        {
            _freeLook.action.Disable();
            _moveAction.action.Disable();
        }

        void Update()
        {
            _movement.SetMoveInput(_moveAction.action.ReadValue<Vector2>());
        }
    }
}
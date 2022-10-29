/**
 * CharacterMovement.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2022-10-29
 */

using UnityEngine;

namespace Source.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField, Range(1, 20)]
        float _speed = 6;
        [SerializeField, Range(.01f, .5f)]
        float _smoothTime;
        [SerializeField, Range(0, 100)]
        float _gravityScale = 1;

        CharacterController _controller;
        Transform _cameraTransform;
        Vector2 _moveInput;
        Vector2 _smoothVelocity;
        float _gravityLastFrame;

        void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        void OnEnable()
        {
            _cameraTransform = Camera.main.transform;
        }

        void Update()
        {
            var moveDirection = _cameraTransform.TransformDirection(new Vector3(_moveInput.x, 0, _moveInput.y));
            moveDirection *= _speed;

            moveDirection.y = IsGrounded() ? 0 : _gravityLastFrame + (_gravityScale * Physics.gravity.y * Time.deltaTime);

            _controller.Move(Time.deltaTime * moveDirection);
            _gravityLastFrame = moveDirection.y;
        }

        public void SetMoveInput(Vector2 input)
        {
            _moveInput = Vector2.SmoothDamp(_moveInput, input, ref _smoothVelocity, _smoothTime);
        }

        public bool IsGrounded()
        {
            if (_controller.isGrounded)
                return true;

            var skinWidth = _controller.skinWidth;
            var ray = new Ray(transform.position + Vector3.up * skinWidth, Vector3.down);
            return Physics.Raycast(ray, _controller.stepOffset + skinWidth * 2, 1);
        }
    }
}
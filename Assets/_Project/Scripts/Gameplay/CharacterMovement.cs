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

        CharacterController _controller;
        Transform _cameraTransform;
        Vector2 _smoothVelocity;
        Vector2 _smoothInput;

        void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        void OnEnable()
        {
            _cameraTransform = Camera.main.transform;
        }

        public void Move(Vector2 input)
        {
            _smoothInput = Vector2.SmoothDamp(_smoothInput, input, ref _smoothVelocity, _smoothTime);
            var dir = _cameraTransform.TransformDirection(new Vector3(_smoothInput.x, 0, _smoothInput.y));
            dir.y = 0;
            _controller.Move(_speed * Time.deltaTime * dir);
        }
    }
}
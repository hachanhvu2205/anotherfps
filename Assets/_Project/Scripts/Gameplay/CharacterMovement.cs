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

        CharacterController _controller;
        Transform _cameraTransform;

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
            var dir = _cameraTransform.TransformDirection(new Vector3(input.x, 0, input.y));
            dir.y = 0;
            _controller.Move(dir * _speed * Time.deltaTime);
        }
    }
}
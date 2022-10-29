/**
 * CameraController.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2022-10-29
 */

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.Universal;

namespace Source.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        Camera _fpsCamera;

        void OnEnable()
        {
            SetFPSCameraEnabled(true);
            SetCursorEnabled(false);
        }

        void OnDisable()
        {
            SetFPSCameraEnabled(false);
            SetCursorEnabled(true);
        }

        public void SetCursorEnabled(bool value)
        {
            Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = value;
        }

        public void SetFPSCameraEnabled(bool value)
        {
            var mainCamera = Camera.main;
            if (!mainCamera)
                return;

            var cameraData = mainCamera.GetComponent<UniversalAdditionalCameraData>();
            var constraint = _fpsCamera.GetComponent<RotationConstraint>();
            if (value)
            {
                cameraData.cameraStack.Add(_fpsCamera);
                constraint.AddSource(new ConstraintSource()
                {
                    sourceTransform = mainCamera.transform,
                    weight = 1
                });
            }
            else
            {
                cameraData.cameraStack.Remove(_fpsCamera);
                constraint.RemoveSource(0);
            }
            constraint.constraintActive = value;
        }
    }
}
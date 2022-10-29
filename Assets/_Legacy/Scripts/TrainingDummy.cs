/**
* TrainingDummy.cs
* Created by: João Borks [joao.borks@gmail.com]
* Created on: 2022-10-29
*/

using System.Collections;
using UnityEngine;

namespace Legacy
{
    public class TrainingDummy : MonoBehaviour
    {
        [SerializeField]
        Renderer _dummyRenderer;
        [SerializeField]
        Gradient _feedbackGradient;
        [SerializeField]
        AnimationCurve _feedbackCurve;
        [SerializeField, Range(.5f, 3)]
        float _feedbackDuration = 1;

        Coroutine _feedbackRoutine;

        void Awake()
        {
            _dummyRenderer = GetComponentInChildren<Renderer>();
        }

        public void TakeDamage()
        {
            if (_feedbackRoutine != null)
                StopCoroutine(_feedbackRoutine);
            _feedbackRoutine = ApplyHitFeedback();
        }

        Coroutine ApplyHitFeedback()
        {
            return StartCoroutine(hitFeedbackRoutine());
            IEnumerator hitFeedbackRoutine()
            {
                var material = _dummyRenderer.material;
                var time = 0f;
                while (time < _feedbackDuration)
                {
                    time += Time.deltaTime;
                    material.color = _feedbackGradient.Evaluate(_feedbackCurve.Evaluate(time / _feedbackDuration));
                    yield return null;
                }
            }
        }
    }
}
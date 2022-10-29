/**
 * RecoilInfo.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2022-10-29
 */

using UnityEngine;

namespace Source.Weapons
{
    [System.Serializable]
    public struct RecoilInfo
    {
        public float Strength;
        public AnimationCurve Curve;
        public float Time;
        public float SustainTime;
    }
}
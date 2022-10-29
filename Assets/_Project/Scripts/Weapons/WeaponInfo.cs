/**
 * WeaponInfo.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2022-10-29
 */

using UnityEngine;

namespace Source.Weapons
{
    [System.Serializable]
    public struct WeaponInfo
    {
        public WeaponType Type;
        public WeaponFamily Family;
        public WeaponFireMode FireMode;
        [Range(1, 300)]
        public int MagazineCapacity;
        [Range(1, 600)]
        public int MaximumAmmo;
        public RecoilInfo Recoil;
        public float RoundsPerSecond;
    }
}
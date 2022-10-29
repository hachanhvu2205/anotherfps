/**
 * WeaponAsset.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2022-10-29
 */

using UnityEngine;

namespace Source.Weapons
{
    [CreateAssetMenu(menuName = "Project/Weapon Asset", fileName = "NewWeapon")]
    public class WeaponAsset : ScriptableObject
    {
        public GameObject Prefab => _prefab;
        public WeaponInfo Info => _info;
        public Vector3 OffsetPosition => _offsetPosition;
        public Vector3 MuzzlePosition => _muzzlePosition;

        [SerializeField]
        GameObject _prefab;
        [SerializeField]
        WeaponInfo _info;
        [SerializeField]
        Vector3 _offsetPosition;
        [SerializeField]
        Vector3 _muzzlePosition;
    }
}
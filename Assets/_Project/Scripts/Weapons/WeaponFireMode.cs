/**
* WeaponFireMode.cs
* Created by: João Borks [joao.borks@gmail.com]
* Created on: 2022-10-29
*/

using System;

namespace Source.Weapons
{
    [Flags]
    public enum WeaponFireMode
    {
        None = 0,
        Single = 0b1,
        Burst = 0b10,
        Auto = 0b100
    }
}
﻿using UnityEngine;
using Zenject;

public class WeaponReloadSound : WeaponSoundPlayer
{
    [Inject] readonly WeaponReload m_weaponReload;

    protected override AudioClip Sound => m_weaponHandler.Weapon_SO.reloadSound;

    protected override void SubscribeToAction()
    {
        m_weaponReload.OnPlayerReloaded += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        m_weaponReload.OnPlayerReloaded -= PlaySound;
    }
}

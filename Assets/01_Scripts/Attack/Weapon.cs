using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected WeaponData data;

    public virtual void Init(WeaponData weaponData)
    {
        data = weaponData;
    }
}

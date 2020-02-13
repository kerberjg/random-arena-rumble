using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TYPE
{
    Weapon_Pistol,
    Weapon_Akimpo,
    Weapon_Shotgun,
    Weapon_Sniper,
    Arena_Fire,
    Arena_Rain,
    Arena_Earthquake,
    Enemy_Melee,
    Enemy_SharpShooter,
}

public class IconType : MonoBehaviour
{
    public TYPE type;
}

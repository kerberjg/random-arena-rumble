﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierWeapons : MonoBehaviour
{
    public enum WeaponType { pistol, sniper, akimbo };
    public int weaponType = 1;

    public GameObject Pistol_LeftHand;
    public GameObject Pistol_RightHand;

    public GameObject Sniper_LeftHand;
    public GameObject Sniper_RightHand;

    public GameObject AkimboHand;
    public GameObject RightHand_Empty;


    // Update is called once per frame
    void Update()
    {

        switch (weaponType) {

            case 1:
                print("Pistol");
                Pistol_LeftHand.SetActive(true);
                Pistol_RightHand.SetActive(true);
                Sniper_LeftHand.SetActive(false);
                Sniper_RightHand.SetActive(false);
                break;
            case 2:
                print("Sniper");
                Sniper_LeftHand.SetActive(true);
                Sniper_RightHand.SetActive(true);
                Pistol_LeftHand.SetActive(false);
                Pistol_RightHand.SetActive(false);

                break;
            case 3:
                print("Akimbo");
                AkimboHand.SetActive(true);
                RightHand_Empty.SetActive(false);
                break;
            default:
                print("Default");
                break;
        }
    }
}

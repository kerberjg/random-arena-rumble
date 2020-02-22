using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : ThrottledMonoBehaviour
{
    public GameObject LeftHand;
    private WeaponType previousLeft;
    public GameObject RightHand;
    private WeaponType previousRight;

    // initialize this script as a throttled behavior
    // (meaning this script will call update only every 2 frames)
    public WeaponManager() : base(2) {}

    protected override void ThrottledUpdate() {
        WeaponType left = ValueModifier.TryGetModifier(this).leftWeapon;
        if(left != previousLeft) {
            previousLeft = left;
            SwitchWeapon(left, LeftHand);
        }
        
        WeaponType right = ValueModifier.TryGetModifier(this).rightWeapon;
        if(right != previousRight) {
            previousRight = right;
            SwitchWeapon(right, RightHand);
        }
    }

    void SwitchWeapon(WeaponType type, GameObject hand) {
        DeactivateAll(hand);

        switch (type) {
            default:
            case WeaponType.none:
                FindChild(hand, "None").SetActive(true);
                break;

            case WeaponType.pistol:
                FindChild(hand, "Pistol").SetActive(true);
                break;

            case WeaponType.sniper:
                FindChild(hand, "Sniper").SetActive(true);
                break;

            case WeaponType.shotgun:
                FindChild(hand, "Shotgun").SetActive(true);
                break;

            case WeaponType.enemyPistol:
                FindChild(hand, "EnemyPistol").SetActive(true);
                break;
        }
    }

    static void DeactivateAll(GameObject parent) {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        bool selfFlag = false;

        foreach(Transform obj in children) {
            if(!selfFlag) {
                selfFlag = true;
                continue;
            }

            obj.gameObject.SetActive(false);
        }
    }

    static GameObject FindChild(GameObject parent, string name) {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        bool selfFlag = false;

        foreach(Transform obj in children) {
            if(!selfFlag) {
                selfFlag = true;
                continue;
            }
            
            if(obj.gameObject.name == name)
                return obj.gameObject;
        }

        return null;
    }
}

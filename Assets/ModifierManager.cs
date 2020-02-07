using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierManager : MonoBehaviour
{
    ModifierWeapons modifierWeapons;
    // Update is called once per frame

    private void Start()
    {
        modifierWeapons = GameObject.Find("Player").GetComponent<ModifierWeapons>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) {
            modifierWeapons.weaponType = 1;
        } else if(Input.GetKey(KeyCode.Alpha2)){
            modifierWeapons.weaponType = 2;
        } else if (Input.GetKey(KeyCode.Alpha3)) {
            modifierWeapons.weaponType = 3;
        }
    }
}

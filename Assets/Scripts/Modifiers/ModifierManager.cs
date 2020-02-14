using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ModifierManager : MonoBehaviour
{
    ModifierWeapons modifierWeapons;
    ModifierPlayer modifierPlayer;
    // Update is called once per frame
    int totalModifers;
    public int nextPlayerModifier;

    public List<int> excludeModifiers = new List<int> { 0, 1 };

    private void Start()
    {
        modifierWeapons = GameObject.Find("Player").GetComponent<ModifierWeapons>();
        modifierPlayer = GameObject.Find("Player").GetComponent<ModifierPlayer>();

        totalModifers = Enum.GetNames(typeof(ModifierWeapons.WeaponType)).Length + Enum.GetNames(typeof(ModifierPlayer.PlayerModifiers)).Length;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {

            modifierPlayer.modifierApplied = false;

            nextPlayerModifier = UnityEngine.Random.Range(1, totalModifers + 1);

            while (excludeModifiers.Contains(nextPlayerModifier)) {
                nextPlayerModifier = UnityEngine.Random.Range(1, totalModifers + 1);
            }

            switch (nextPlayerModifier) {
                case 1:
                    print("First");
                    modifierWeapons.currentWeapon = ModifierWeapons.WeaponType.pistol;
                    excludeModifiers.Add(nextPlayerModifier); //Player can't recieve pistol because player has pistol.
                    excludeModifiers.Remove(5); //Player can now recieve Sniper again!
                    break;
                case 2:
                    print("Second");
                    modifierPlayer.nextModifier = ModifierPlayer.PlayerModifiers.speedDown;
                    modifierPlayer.ApplyModifierPlayer();
                    break;
                case 3:
                    print("Third");
                    modifierWeapons.currentWeapon = ModifierWeapons.WeaponType.akimbo;
                    excludeModifiers.Add(nextPlayerModifier); //Player can't recieve Akimbo anymore because player has akimbo.
                    break;
                case 4:
                    print("Fourth");
                    modifierPlayer.nextModifier = ModifierPlayer.PlayerModifiers.speedUp;
                    modifierPlayer.ApplyModifierPlayer();
                    break;
                case 5:
                    print("Fifth");
                    modifierWeapons.currentWeapon = ModifierWeapons.WeaponType.sniper;
                    excludeModifiers.Add(nextPlayerModifier); //Player can't recieve sniper because player has sniper.
                    excludeModifiers.Remove(1); //Player can now recieve Pistol again!
                    break;
                case 6:
                    print("Sixth");
                    modifierPlayer.nextModifier = ModifierPlayer.PlayerModifiers.sizeUp;
                    modifierPlayer.ApplyModifierPlayer();
                    break;
                case 7:
                    print("Seventh");
                    modifierPlayer.nextModifier = ModifierPlayer.PlayerModifiers.sizeDown;
                    modifierPlayer.ApplyModifierPlayer();
                    break;
            }
        }
        

        //MANUAL MODIFIER SETTINGS FOR TESTING
        if (Input.GetKey(KeyCode.Alpha1)) {
            modifierWeapons.currentWeapon = ModifierWeapons.WeaponType.pistol; //SWAP TO PISTOL
        } else if(Input.GetKey(KeyCode.Alpha2)){
            modifierWeapons.currentWeapon = ModifierWeapons.WeaponType.sniper; //SWAP TO SNIPER
        } else if (Input.GetKey(KeyCode.Alpha3)) {
            modifierWeapons.currentWeapon = ModifierWeapons.WeaponType.akimbo; //SWAP TO AKIMBO
        }

        if (Input.GetKey(KeyCode.Alpha0) && modifierPlayer.modifierApplied == false) { //PLAYER SPEED DOWN
            modifierPlayer.nextModifier = ModifierPlayer.PlayerModifiers.speedDown;
            modifierPlayer.ApplyModifierPlayer();
        }

        if (Input.GetKey(KeyCode.Alpha9) && modifierPlayer.modifierApplied == false) { //PLAYER SPEED UP
            modifierPlayer.nextModifier = ModifierPlayer.PlayerModifiers.speedUp;
            modifierPlayer.ApplyModifierPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) {  // PLAYER SIZE UP

            modifierPlayer.SizeUp();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) {  //PLAYER SIZE DOWN

            modifierPlayer.SizeDown();
        }

    }
}

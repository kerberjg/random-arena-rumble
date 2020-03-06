using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Sniper : MonoBehaviour
{
    ModifierContainer modifierContainer;

    private void Start()
    {
        modifierContainer = GameObject.Find("Player").GetComponent<ModifierContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {

            ActivateSniper();
            
        }
    }

    private void ActivateSniper()
    {
        modifierContainer.modifier.rightWeapon = WeaponType.sniper;

        Destroy(gameObject);
    }
}

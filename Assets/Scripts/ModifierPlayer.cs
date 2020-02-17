using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierPlayer : MonoBehaviour
{
    ModifierManager modifierManager;
    PlayerMovement playerMovement;
    Hurtbox playerHurtBox;
    Transform playerTransform;

    public float maxSpeed;
    public float minSpeed;
    float normalSpeed;

    public float maxSize;
    public float minSize;
    float normalSize;

    public float giantHealth;
    public float tinyHealth;
    float normalHealth;

    public bool modifierApplied;


    public enum PlayerModifiers { speedUp, speedDown, sizeUp, sizeDown } //damage, health
    public PlayerModifiers nextModifier;
    // Start is called before the first frame update
    void Start()
    {
        modifierManager = GameObject.Find("ModifierManager").GetComponent<ModifierManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHurtBox = GameObject.Find("Player").GetComponent<Hurtbox>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        normalSpeed = playerMovement.speed;
        normalSize = playerTransform.localScale.x;
        normalHealth = playerHurtBox.maxHealth;
    }

    private void Update()
    {

    }

    private void HealthModifier()
    {
        if (playerTransform.localScale.x == normalSize) {
            playerHurtBox.maxHealth = normalHealth;
        } else if (playerTransform.localScale.x == maxSize) {
            playerHurtBox.maxHealth = giantHealth;
        } else if (playerTransform.localScale.x == minSize) {
            playerHurtBox.maxHealth = tinyHealth;
        }
        playerHurtBox.health = playerHurtBox.maxHealth;
    }

    public void SizeDown()
    {
        if (playerTransform.localScale.x == normalSize) {
            playerTransform.localScale = new Vector3(minSize, minSize, minSize);
            modifierManager.excludeModifiers.Add(7); //Player has min Size, can't get SizeDown anymore.
        } else if (playerTransform.localScale.x == maxSize) {
            playerTransform.localScale = new Vector3(normalSize, normalSize, normalSize);

        }

        if(playerTransform.localScale.x != maxSize) {
            modifierManager.excludeModifiers.Remove(6); //Player can get Size Up.
        }

        HealthModifier();
    }

    public void SizeUp()
    {
        if (playerTransform.localScale.x == normalSize) {
            playerTransform.localScale = new Vector3(maxSize, maxSize, maxSize);
            modifierManager.excludeModifiers.Add(6); //Player has max Size, can't get SizeUp anymore.
        } else if (playerTransform.localScale.x == minSize) {
            playerTransform.localScale = new Vector3(normalSize, normalSize, normalSize);
        }

        if(playerTransform.localScale.x != minSize) {
            modifierManager.excludeModifiers.Remove(7); //Player can get Size Down.
        }

        HealthModifier();
    }

    public void ApplyModifierPlayer()
    {
        switch (nextModifier) {
            case PlayerModifiers.speedUp:
                SpeedUp();
                break;
            case PlayerModifiers.speedDown:
                SpeedDown();
                break;
            case PlayerModifiers.sizeUp:
                SizeUp();
                break;
            case PlayerModifiers.sizeDown:
                SizeDown();
                break;
        }
    }

    private void SpeedUp()
    {
           
            if (playerMovement.speed == normalSpeed) {
                    playerMovement.speed = maxSpeed;
                    modifierManager.excludeModifiers.Add(4); //Player has max Speed, can't get Speed Up anymore.       
            
            } else if (playerMovement.speed == minSpeed) {
                    playerMovement.speed = normalSpeed;           
            }

            if(playerMovement.speed != minSpeed) {
                 modifierManager.excludeModifiers.Remove(2); //Player can get Speed Down.
            }
            modifierApplied = true;
             
        
    }

    private void SpeedDown()
    {
            
            if (playerMovement.speed == normalSpeed) {
                playerMovement.speed = minSpeed;
                modifierManager.excludeModifiers.Add(2); //Player has min Speed, can't get Speed Down anymore.
                
            } else if (playerMovement.speed == maxSpeed) {
                playerMovement.speed = normalSpeed;
            }

            if(playerMovement.speed != maxSpeed) {
                modifierManager.excludeModifiers.Remove(4); //Player can get Speed Up.
            }
            modifierApplied = true;
        
    }

}

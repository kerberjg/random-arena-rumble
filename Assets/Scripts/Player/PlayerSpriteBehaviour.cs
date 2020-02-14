using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteBehaviour : MonoBehaviour
{

    public SpriteRenderer playerBody;
    public SpriteRenderer playerHead;

    [Header("Torso sprites")]
    public Sprite rSprite_Torso;
    public Sprite ruSprite_Torso;
    public Sprite uSprite_Torso;
    public Sprite ulSprite_Torso;
    public Sprite lSprite_Torso;
    public Sprite ldSprite_Torso;
    public Sprite dSprite_Torso;
    public Sprite drSprite_Torso;

    [Header("Head sprites")]
    public Sprite rSprite_Head;
    public Sprite ruSprite_Head;
    public Sprite uSprite_Head;
    public Sprite ulSprite_Head;
    public Sprite lSprite_Head;
    public Sprite ldSprite_Head;
    public Sprite dSprite_Head;
    public Sprite drSprite_Head;

    Transform playerPosition;

    [Header("Rotation settings")]
    [Range(4,8)] public int rotationSteps = 4;

    [SerializeField] private float currentStep;

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();

        Vector2 mousePos = MouseUtils.GetMousePosition();
        float aimAngle = MouseUtils.GetAimAngle(transform.position) * Mathf.Rad2Deg + 180;
        int stepAngle = Mathf.RoundToInt(360f / rotationSteps);
        currentStep = Mathf.RoundToInt( aimAngle / stepAngle ) % 4 * stepAngle;

        switch(currentStep) {
            default:
            case 0:
            case 360:
                playerBody.sprite = rSprite_Torso;
                playerHead.sprite = rSprite_Head;
                break;
            
            case 45:
                playerBody.sprite = ruSprite_Torso;
                playerHead.sprite = ruSprite_Head;
                break;

            case 90:
                playerBody.sprite = uSprite_Torso;
                playerHead.sprite = uSprite_Head;
                break;

            case 135:
                playerBody.sprite = ulSprite_Torso;
                playerHead.sprite = ulSprite_Head;
                break;

            case 180:
                playerBody.sprite = lSprite_Torso;
                playerHead.sprite = lSprite_Head;
                break;

            case 225:
                playerBody.sprite = ldSprite_Torso;
                playerHead.sprite = ldSprite_Head;
                break;
            
            case 270:
                playerBody.sprite = dSprite_Torso;
                playerHead.sprite = dSprite_Head;
                break;

            case 315:
                playerBody.sprite = drSprite_Torso;
                playerHead.sprite = drSprite_Head;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteBehaviour : MonoBehaviour
{

    public SpriteRenderer playerBody;
    public SpriteRenderer playerHead;

    public Sprite leftSprite_Torso;
    public Sprite rightSprite_Torso;
    public Sprite upSprite_Torso;
    public Sprite downSprite_Torso;

    public Sprite leftSprite_Head;
    public Sprite rightSprite_Head;
    public Sprite upSprite_Head;
    public Sprite downSprite_Head;

    Transform playerPosition;

    public float xOffset;
    public float yOffset;
    public int rotationSteps = 4;

    [SerializeField] private float currentStep;

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();

        Vector2 mousePos = MouseUtils.GetMousePosition();
        float aimAngle = MouseUtils.GetAimAngle(transform.position) * Mathf.Rad2Deg + 180;
        currentStep = Mathf.RoundToInt( aimAngle / (360f / rotationSteps) ) % 4;

        switch(currentStep) {
            default:
            case 0:
                playerBody.sprite = rightSprite_Torso;
                playerHead.sprite = rightSprite_Head;
                break;
            case 1:
                playerBody.sprite = upSprite_Torso;
                playerHead.sprite = upSprite_Head;
                break;
            case 2:
                playerBody.sprite = leftSprite_Torso;
                playerHead.sprite = leftSprite_Head;
                break;
            case 3:
                playerBody.sprite = downSprite_Torso;
                playerHead.sprite = downSprite_Head;
                break;
        }
    }
}

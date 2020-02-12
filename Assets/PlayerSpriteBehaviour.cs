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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();

        Vector2 mousePos = GetMousePosition();
        Debug.Log(mousePos);

        if (mousePos.x < playerPosition.position.x - xOffset) {
            playerBody.sprite = leftSprite_Torso;
            playerHead.sprite = leftSprite_Head;
        } else if (mousePos.x < playerPosition.position.x && mousePos.x > playerPosition.position.x - xOffset && mousePos.y > playerPosition.position.y) {
            playerBody.sprite = upSprite_Torso;
            playerHead.sprite = upSprite_Head;
        } else if(mousePos.x > playerPosition.position.x + xOffset) {
            playerBody.sprite = rightSprite_Torso;
            playerHead.sprite = rightSprite_Head;
        } else if(mousePos.x > playerPosition.position.x && mousePos.x < playerPosition.position.x + xOffset && mousePos.y < playerPosition.position.y) {
            playerBody.sprite = downSprite_Torso;
            playerHead.sprite = downSprite_Head;
        }
    }

    public static Vector2 GetMousePosition() {
        Vector3 tmpPos = Input.mousePosition;
        tmpPos.z = 0 - Camera.main.transform.position.z; 
        return Camera.main.ScreenToWorldPoint(tmpPos);
    }
}

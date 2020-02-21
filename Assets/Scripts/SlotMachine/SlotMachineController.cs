using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotMachineState {
    idle,
    spinning,
    done,
    end
}

public class SlotMachineController : MonoBehaviour {
    [Header("Visuals")]
    public JoystickController lever;
    public Transform bigCylinder;
    public Transform smallCylinder;
    public float spinningSpeed = 500f;
    public SpriteRenderer playerSlot;
    public SpriteRenderer arenaSlot;
    public SpriteRenderer enemySlot;

    [Header("State")]
    public SlotMachineState state = SlotMachineState.idle;
    
    public float waitAfterRoll = 2f;
    public float animationLength = 5f;
    [SerializeField] private float animationCounter;

    void Start()
    {
        Transform parent = GameObject.Find("ModifiersContainer").transform;

        Transform slot1 = parent.Find("slot_1");
        for (int i = 0;i< slot1.childCount; i++)
        {
            playerModifierOptions.Add(slot1.GetChild(i).gameObject);
        }
        Transform slot2 = parent.Find("slot_2");
        for (int i = 0; i < slot2.childCount; i++)
        {
            arenaModifierOptions.Add(slot2.GetChild(i).gameObject);
        }
        Transform slot3 = parent.Find("slot_3");
        for (int i = 0; i < slot3.childCount; i++)
        {
            enemyModifierOptions.Add(slot3.GetChild(i).gameObject);
        }
    }

    void Update() {
        animationCounter -= Time.deltaTime;

        switch(state) {
            // wait for player to touch lever
            default:
            case SlotMachineState.idle:
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) {
                    state = SlotMachineState.spinning;
                    animationCounter = animationLength;
                    lever.Press();
                    SoundManager.i.PlayOnce("Spinning");

                    // hide slots
                    playerSlot.gameObject.SetActive(false);
                    arenaSlot.gameObject.SetActive(false);
                    enemySlot.gameObject.SetActive(false);
                }
                return;

            // spinspinspinspiiin
            case SlotMachineState.spinning:
                // rotate cylinders
                bigCylinder.transform.Rotate(Vector3.up * spinningSpeed * Time.deltaTime);
                smallCylinder.transform.Rotate(Vector3.up * spinningSpeed * Time.deltaTime);

                float slotTime = animationLength / 3f;
                if(animationCounter <= slotTime * 0 && !enemySlot.gameObject.activeSelf) {
                    PickEnemyModifier();
                    state = SlotMachineState.done;
                    animationCounter = waitAfterRoll;
                    SoundManager.i.StopPlaying("Spinning");
                } else if(animationCounter <= slotTime * 1 && !arenaSlot.gameObject.activeSelf) {
                    PickArenaModifier();
                } else if(animationCounter <= slotTime * 2 && !playerSlot.gameObject.activeSelf) {
                    PickPlayerModifier();
                }
                break;

            // wait after results roll
            case SlotMachineState.done:
                if(animationCounter <= 0f) {
                    state = SlotMachineState.end;
                }
                break;

            // set modifiers and start game
            case SlotMachineState.end:
                print(GameManager.playerModifier.health);
                print(this.playerModifier.health);
                GameManager.playerModifier.MergeModifier(this.playerModifier);
                GameManager.arenaModifier.MergeModifier(this.arenaModifier);
                GameManager.enemyModifier.MergeModifier(this.enemyModifier);

                print(GameManager.playerModifier.health);
                GameManager.instance.ToArenaScene();
                break;
        }
    }

    [Header("Potential modifiers")]
    private List<GameObject> playerModifierOptions = new List<GameObject>();
    private List<GameObject> arenaModifierOptions = new List<GameObject>();
    private List<GameObject> enemyModifierOptions = new List<GameObject>();

    [Header("Final modifiers")]
    private ValueModifier playerModifier;
    private ValueModifier arenaModifier;
    private ValueModifier enemyModifier;

    void PickPlayerModifier() {
        // get modifier
        GameObject obj = playerModifierOptions[Random.Range(0, playerModifierOptions.Count)];
        playerModifier = obj.GetComponent<ModifierContainer>().modifier;

        // set sprite
        playerSlot.sprite = playerModifier.icon;
        playerSlot.gameObject.SetActive(true);
    }

    void PickArenaModifier() {
        // get modifier
        GameObject obj = arenaModifierOptions[Random.Range(0, arenaModifierOptions.Count)];
        arenaModifier = obj.GetComponent<ModifierContainer>().modifier;

        // set sprite
        arenaSlot.sprite = arenaModifier.icon;
        arenaSlot.gameObject.SetActive(true);
    }

    void PickEnemyModifier() {
        // get modifier
        GameObject obj = enemyModifierOptions[Random.Range(0, enemyModifierOptions.Count)];
        enemyModifier = obj.GetComponent<ModifierContainer>().modifier;

        // set sprite
        enemySlot.sprite = enemyModifier.icon;
        enemySlot.gameObject.SetActive(true);
    }
}
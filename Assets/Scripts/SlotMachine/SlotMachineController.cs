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
    public Transform lever;
    public Transform bigCylinder;
    public Transform smallCylinder;
    public float spinningSpeed;
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
        Transform parent = transform.Find("ModifiersContainer");

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
                    // SoundManager.i.PlayOnce("Spinning");
                }
                return;

            // spinspinspinspiiin
            case SlotMachineState.spinning:
                float slotTime = animationLength / 3f;
                if(animationLength <= slotTime * 0) {
                    PickEnemyModifier();
                    state = SlotMachineState.done;
                    // SoundManager.i.StopPlaying("Spinning");
                } else if(animationLength <= slotTime * 1) {
                    PickArenaModifier();
                } else if(animationLength <= slotTime * 2) {
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
    private ArenaModifier arenaModifier;
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
        arenaModifier = obj.GetComponent<ModifierContainer>().arena;

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
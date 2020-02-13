using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    public List<GameObject> SlotOne = new List<GameObject>();
    public List<GameObject> SlotTwo = new List<GameObject>();
    public List<GameObject> SlotThree = new List<GameObject>();

    public List<int> result = new List<int>();

    public bool FirstDisplayed = false;
    public float FirstDelay = 1.0f;
    public float accumulatedTime_1 = 0.0f;

    public bool SecondDisplayed = false;
    public float SecondAndThirdDelay = 0.5f;
    public float accumulatedTime_2 = 0.0f;

    public bool onProcess = false;
    private void Start()
    {
        Transform parent = transform.Find("Slots");

        Transform slot1 = parent.Find("slot_1");
        for (int i = 0;i< slot1.childCount; i++)
        {
            SlotOne.Add(slot1.GetChild(i).gameObject);
        }
        Transform slot2 = parent.Find("slot_2");
        for (int i = 0; i < slot2.childCount; i++)
        {
            SlotTwo.Add(slot2.GetChild(i).gameObject);
        }
        Transform slot3 = parent.Find("slot_3");
        for (int i = 0; i < slot3.childCount; i++)
        {
            SlotThree.Add(slot3.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onProcess == false)
        {
            onProcess = true;
            GetRandomResult();
        }

        if(onProcess == true)
        {
            accumulatedTime_1 += Time.deltaTime;
            if(accumulatedTime_1 >= FirstDelay)
            {
                SlotOne[result[0]].SetActive(true);
                FirstDisplayed = true;
                accumulatedTime_1 = 0.0f;
            }

            if(FirstDisplayed == true)
            {
                accumulatedTime_2 += Time.deltaTime;
                if (accumulatedTime_2 >= SecondAndThirdDelay)
                {
                    SlotTwo[result[1]].SetActive(true);
                    SecondDisplayed = true;
                    accumulatedTime_2 = 0.0f;
                }
            }

            if(SecondDisplayed == true)
            {
                accumulatedTime_2 += Time.deltaTime;
                if (accumulatedTime_2 >= SecondAndThirdDelay)
                {
                    SlotThree[result[2]].SetActive(true);
                    accumulatedTime_2 = 0.0f;
                    onProcess = false;
                }
            }
        }
    }

    public void GetRandomResult()
    {
        accumulatedTime_1 = 0.0f;
        accumulatedTime_2 = 0.0f;
        SecondDisplayed = false;
        FirstDisplayed = false;

        foreach(GameObject element in SlotOne)
        {
            element.SetActive(false);
        }
        foreach (GameObject element in SlotTwo)
        {
            element.SetActive(false);
        }
        foreach (GameObject element in SlotThree)
        {
            element.SetActive(false);
        }

        result.Clear();
        result.Add(Random.Range(0, SlotOne.Count));
        result.Add(Random.Range(0, SlotTwo.Count));
        result.Add(Random.Range(0, SlotThree.Count));

        PrintResult();
    }

    public void PrintResult()   //To use the data of the slot, use GetComponent<IconType> showed below
    {
        print("Slot 1 is " + SlotOne[result[0]].GetComponent<IconType>().type + "\n");
        print("Slot 2 is " + SlotTwo[result[1]].GetComponent<IconType>().type + "\n");
        print("Slot 3 is " + SlotThree[result[2]].GetComponent<IconType>().type + "\n");
    }
}
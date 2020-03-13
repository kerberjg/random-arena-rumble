using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxTrigger : MonoBehaviour
{
    public UnityEvent events;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            events.Invoke();
            GetComponent<Collider2D>().enabled = false;
        }
    }
}

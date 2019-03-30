using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.StartListening("PressurePlateActivated", OpenDoor);
        EventManager.StartListening("PressurePlateReleased", CloseDoor);
    }

    void OnDisable()
    {
        EventManager.StopListening("PressurePlateActivated", OpenDoor);
        EventManager.StopListening("PressurePlateReleased", CloseDoor);
    }

    void OpenDoor()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        BoxCollider2D colider = GetComponent<BoxCollider2D>();
        colider.enabled = !colider.enabled;
        Debug.Log("OpenDoor Function was called!");
    }

    void CloseDoor()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.blue;
        BoxCollider2D colider = GetComponent<BoxCollider2D>();
        colider.enabled = colider.enabled;
        Debug.Log("CloseDoor Function was called!");
    }
}

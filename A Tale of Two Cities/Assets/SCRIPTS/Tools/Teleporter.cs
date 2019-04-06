using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public int padCode;
    float disableTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (disableTimer > 0)
            disableTimer -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player" && disableTimer<=0)
        {
            foreach (Teleporter tp in FindObjectsOfType<Teleporter>())
            {
                if(tp.padCode == padCode && tp != this)
                {
                    Debug.Log("Player teleported");
                    tp.disableTimer = 2;
                    Vector3 padPosition = tp.gameObject.transform.position;
                    collider.gameObject.transform.position = padPosition;
                }
            }
        }
    }

    /*public Transform destination;
    public string tagList = "|Player|Enemy|";
    float disableTimer = 0f;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (tagList.Contains(string.Format("|Player|Enemy|")) && disableTimer <= 0)
        {
            
            collider.transform.position = destination.transform.position;
        }
    }

    void Update()
    {
        if (disableTimer > 0f)
        {
            disableTimer -= Time.deltaTime;
        }
    }*/
}

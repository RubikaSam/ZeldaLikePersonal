using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        DifferentScene, SameScene,
    }

    public TransitionType transitionType;

    public bool stay = false;
    public int padCode;

    public Vector3 padPosition;
    float disableTimer = 0.0f;
    public bool isTeleporting = false;
    public bool justTeleported = false;
    void Update()
    {
        if (disableTimer > 0)
            disableTimer -= Time.deltaTime;
            else{
                justTeleported = false;
            }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        isTeleporting = true;

            if(collider.gameObject.tag == "Player" && !justTeleported)
        {
            foreach (TransitionPoint tp in FindObjectsOfType<TransitionPoint>())
            {
                if(tp.padCode == padCode && tp != this)
                {
                    PlayerController controler = new PlayerController();

                    Debug.Log("Player teleported");
                    tp.disableTimer = 10f;
                    Vector3 padPosition = tp.gameObject.transform.position;
                    collider.transform.position = padPosition;

                    Component bColider = gameObject.GetComponent(typeof(Collider2D));
                    bColider.setActive.false;

                    isTeleporting = false;
                    justTeleported = true;
                }
            }
        }

    }
}
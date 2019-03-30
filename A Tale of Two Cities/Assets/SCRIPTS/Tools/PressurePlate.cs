using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour

{

    [SerializeField]
    public class CaughtObject
    {
        public Rigidbody2D rigidbody;
        public Collider2D collider;
        public bool inContact;
    }

    List<CaughtObject> caughtObjects;


    public enum ActivationType
    {
        Mass
    }

    public ActivationType activationType;
    public float requiredMass = 1f;
    public Sprite activatedPadSprite;
    public Sprite releasedPadSprite;
    public GameObject Door;
    public DoorOpener Script;

    bool WasPreviouslyActivated = false;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (activationType == ActivationType.Mass)
        {
            if (collider.attachedRigidbody.mass >= requiredMass)
            {
                if(!WasPreviouslyActivated)
                {
                    WasPreviouslyActivated = true;
                    EventManager.TriggerEvent("PressurePlateActivated");
                    
                }
            }

            else
            {
                if (WasPreviouslyActivated == true)
                {
                    WasPreviouslyActivated = false;
                    EventManager.TriggerEvent("PressurePlateReleased");
                }
            }

        }
    }

}


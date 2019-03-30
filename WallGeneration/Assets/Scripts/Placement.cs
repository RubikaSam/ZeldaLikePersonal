using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    private int selectedObjectInArray;
    public GameObject currentlySelectedObject;

    [SerializeField]
    private GameObject[] selectableObjects;

    private bool isAnObjectSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        selectedObjectInArray = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 spawnPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        if (Input.GetKeyDown("e") && isAnObjectSelected == false)
        {
            currentlySelectedObject = (GameObject)Instantiate(selectableObjects[selectedObjectInArray], spawnPos, Quaternion.identity);
            isAnObjectSelected = true;
        }

        if (Input.GetMouseButtonDown(1) && isAnObjectSelected == true)
        {
            Destroy(currentlySelectedObject);
            isAnObjectSelected = false;
            selectedObjectInArray = 0;
        }

        //Possible support for multiple objects to scroll between.
        if (Input.GetAxis("Mouse ScrollWheel")> 0 && isAnObjectSelected == true)
        {
            selectedObjectInArray++;
            
            if(selectedObjectInArray > selectableObjects.Length-1)
            {
                selectedObjectInArray = 0;
            }

            Destroy(currentlySelectedObject);
            currentlySelectedObject = (GameObject)Instantiate(selectableObjects[selectedObjectInArray], spawnPos, Quaternion.Euler(0, 0, 90));
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && isAnObjectSelected == true)
        {
            selectedObjectInArray--;

            if (selectedObjectInArray < 0)
            {
                selectedObjectInArray = selectableObjects.Length - 1;
            }

            Destroy(currentlySelectedObject);
            currentlySelectedObject = (GameObject)Instantiate(selectableObjects[selectedObjectInArray], spawnPos, Quaternion.Euler(0, 0, 0));
        }
    }
}

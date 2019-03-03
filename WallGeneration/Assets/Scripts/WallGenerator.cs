using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GameObject WallPrefab;

    public bool isUsingWallAbility = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.A))
        {
            isUsingWallAbility = true;

            if (isUsingWallAbility == true)
            {
                Debug.Log("IsTrue");

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Wall");
                    Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    spawnPosition.z = 0.0f;
                    GameObject objectInstance = Instantiate(WallPrefab, spawnPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
                    isUsingWallAbility = false;
                }
            }
            
        }

        
        
            if(isUsingWallAbility == false)
            {
                Debug.Log("False!");
            }
        
    }
}

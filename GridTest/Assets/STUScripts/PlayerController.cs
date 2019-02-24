using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*public GameObject playerObject;

    public GameObject CellPrefab;

    public void NeighborRecognition(SquareCellScript[] cells)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            NeighborRecognition(cells[i]);
        }
    }

    public void NeighborRecognition(SquareCellScript cell)
    {
        SquareCellScript neighborN;
        SquareCellScript neighborE;
        SquareCellScript neighborS;
        SquareCellScript neighborW;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerObject.transform.position = neighborN.Vector3;
        }
    }


    void Update()
    {
        int layerMaskCells = LayerMask.GetMask("Cells");

        Vector3 fwd = playerObject.transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(playerObject.transform.position, fwd * 1, Color.green);

        RaycastHit hit;

        if (Physics.Raycast(playerObject.transform.position, fwd, out hit, 1, layerMaskCells))
        {
            TouchCell(hit.point);
            //hit.transform.SendMessage("HitByRay");
            //Debug.Log("Contact");
        }

      
    }

    void TouchCell(Vector3 position)
    {
        //position = transform.InverseTransformPoint(position);
        CellCoords coordinates = CellCoords.FromPosition(position);
        Debug.Log("transform contact at" + coordinates.ToString());
    }*/


}

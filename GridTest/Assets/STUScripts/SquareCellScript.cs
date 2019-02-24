using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareCellScript : MonoBehaviour
{
    public CellCoords coordinates;

    public Color color;

    [SerializeField]
    SquareCellScript[] neighbors;

    public SquareCellScript GetNeighbor(CellDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(CellDirection direction, SquareCellScript cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

    public void Neighbors (SquareCellScript cell)
    {
        Debug.Log("Hit by ray");
        SquareCellScript neighborN = cell.GetNeighbor(CellDirection.N);
        SquareCellScript neighborE = cell.GetNeighbor(CellDirection.E);
        SquareCellScript neighborS = cell.GetNeighbor(CellDirection.S);
        SquareCellScript neighborW = cell.GetNeighbor(CellDirection.W);
    }
}

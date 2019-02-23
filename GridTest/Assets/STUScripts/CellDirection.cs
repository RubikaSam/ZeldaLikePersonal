using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellDirection
{
    N, E, S, W
}

//Makes neighbor identification bidirectional
//Defines "direction" (i.e. the number of Elements in the SquareCellScript Array showing neighbors)

public static class CellDirectionExtensions
{

    public static CellDirection Opposite(this CellDirection direction)
    {
        return (int)direction < 2 ? (direction + 2) : (direction - 2);
    }
}

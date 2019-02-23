using UnityEngine;

[System.Serializable]
public struct CellCoords
{

    [SerializeField]
    private int x, y;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
    }

    public CellCoords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }


    //Called by GridScript to convert Raycast touch position to cell coordinates
    //Defines which x y z coordinates belong to a cell position by defining the area of the cell
    public static CellCoords FromPosition (Vector3 position)
    {
        float x = position.x / (SquareMetrics.bisectSegment * 2f);
        float y = position.y / (SquareMetrics.bisectSegment * 2f);

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);

        return new CellCoords(iX, iY);
    }

    public static CellCoords FromCoordinates(int x, int y)
    {
        return new CellCoords(x, y);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString();
    }
}

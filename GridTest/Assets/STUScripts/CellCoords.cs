﻿using UnityEngine;

[System.Serializable]
public struct CellCoords
{

    [SerializeField]
    private int x, z;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Z
    {
        get
        {
            return z;
        }
    }

    public int Y
    {
        get
        {
            return -X - Z;
        }
    }

    public CellCoords(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static CellCoords FromCoordinates(int x, int z)
    {
        return new CellCoords(x, z);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Z.ToString();
    }
}

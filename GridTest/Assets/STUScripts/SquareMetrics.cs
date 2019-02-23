using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SquareMetrics
{
    public const float bisectSegment = 0.5f;

    public static Vector3[] corners =
    {
        new Vector3 (-5f, bisectSegment, 0f),
        new Vector3 (5f, bisectSegment, 0f),
        new Vector3 (5f, -bisectSegment, 0f),
        new Vector3 (-5f, -bisectSegment, 0f)
    };
}

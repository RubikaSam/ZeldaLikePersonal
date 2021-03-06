﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SquareMetrics
{
    public const float bisectSegment = 0.5f;
    public const float diagonal = 0.70710678118f;

    public static Vector3[] corners =
    {
        new Vector3 (bisectSegment, -0.5f * diagonal, 0f),
        new Vector3 (bisectSegment, 0.5f * diagonal, 0f),
        new Vector3 (-bisectSegment, 0.5f * diagonal, 0f),
        new Vector3 (-bisectSegment, -0.5f * diagonal, 0f),
        new Vector3 (bisectSegment, -0.5f * diagonal, 0f)
    };
}

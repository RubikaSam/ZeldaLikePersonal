﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SandTextScript : MonoBehaviour
{
    //Text text;
    public static int sandAmount;

    void Start()
    {
        //text = GetComponent<Text>();
    }
    void Update()
    {
        //text.text = sandAmount.ToString();
        if (sandAmount < 0)
        {
            SceneManager.LoadScene("GameOver");
            sandAmount = 0;
        }

        if (sandAmount > 0)
        {
            Debug.Log("Current sand:"+ sandAmount);
        }
    }
}
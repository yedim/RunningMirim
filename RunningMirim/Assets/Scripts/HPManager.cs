﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour {

    //HP
    public Image fillImage;
    public float timeAmt = 100f;
    public float time;
    public bool updateTimer = true;


    void Start()
    {
        time = timeAmt;
    }

    void Update()
    {
        if (time > 0 && updateTimer == true)
        {
            time -= Time.deltaTime;
            fillImage.fillAmount = time / timeAmt;
        }
    }
    public void ResetHP()
    {
        time = timeAmt;
        fillImage.fillAmount = 1;
    }
  
}
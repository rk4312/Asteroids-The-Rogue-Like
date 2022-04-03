﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationButton : MonoBehaviour
{
    public float score;
    public float cost;

    // Start is called before the first frame update
    void Start()
    {
        cost = 1500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("on mouse down detected");

        score = GlobalObject.Instance.resources;

        if (score >= cost)
        {
            score -= cost;
            
            // Passes the score to global object
            GlobalObject.Instance.resources = score;

            // Reduces time delay between bullets
            GlobalObject.Instance.accelerationRate += 0.0005f; 
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;


public class GameManager : MonoBehaviour
{

 public Transform[] slimeWayPoints;
    public static GameManager instance;
  

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
      
    }

    void Start()
    {
       
    }

    
    void Update()
    {
        
    }
}

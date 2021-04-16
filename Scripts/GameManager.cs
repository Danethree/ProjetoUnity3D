using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;


public class GameManager : MonoBehaviour
{

 public Transform[] slimeWayPoints;
    public static GameManager instance;
    public  float slimeIdleWaitTime = 3f;
    private Transform player;
    public float slimeDistanceToAttack = 2.5f;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
      
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    public Vector3 getPlayer()
    {
        return player.position;
    }
    
    void Update()
    {
        
    }
}

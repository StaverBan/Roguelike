﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject DoorU;
    public GameObject DoorD;
    public GameObject DoorL;
    public GameObject DoorR;

    public void RotateRandom()
    {
        int count = Random.Range(0, 4);

        for (int i = 0; i < count; i++){
            transform.Rotate(0, 0, -90);

            GameObject tmp = DoorL;
            DoorL = DoorD;
            DoorD = DoorR;
            DoorR = DoorU;
            DoorU = tmp ;
            
        }
    }
}

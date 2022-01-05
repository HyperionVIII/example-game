using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    //This script is used to destroy excess particle systems
    public int time;
    void Start()
    {
        Destroy(gameObject, time);
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed;
    
    void FixedUpdate()
    {
        transform.Rotate(0,0,-3 * speed);
    }
}

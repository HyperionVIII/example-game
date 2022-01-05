using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    private Animator anim;
    private float waitTime;
    public float startWaitTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        waitTime = startWaitTime;
    }


    void Update()
    {
        if(waitTime <= 0)
        {
            waitTime = startWaitTime;
            anim.SetTrigger("attack");
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}

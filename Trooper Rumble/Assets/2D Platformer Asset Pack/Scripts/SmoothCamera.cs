using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public Vector3 cameraPos;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
     
    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(cameraPos);
     
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void Shake()
    {
        anim.SetTrigger("shake");
    }
}

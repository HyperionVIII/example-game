using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private Transform destination;

    public bool isGreen;
    public float distance = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        if (isGreen == false)
        {
            destination = GameObject.FindGameObjectWithTag("Green Portal").GetComponent<Transform>();
        }
        else 
        {
            destination = GameObject.FindGameObjectWithTag("Purple Portal").GetComponent<Transform>();
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            other.transform.position = new Vector2(destination.position.x, destination.position.y);
        }
    }

}

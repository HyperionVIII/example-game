using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private Rigidbody2D rigidbody1;
    private PlayerController playerController;
    private UnityEngine.Object bulletParticles;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        rigidbody1 = GetComponent<Rigidbody2D>();
        if(playerController.facingRight) rigidbody1.velocity = new Vector2(1,0) * speed;
        else rigidbody1.velocity = new Vector2(-1,0) * speed;
        bulletParticles = Resources.Load("BulletDestroy");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy") //Destroying the enemy and the bullet
        {
            col.gameObject.GetComponent<EnemyDestroy>().DamageEnemy();
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Untagged") //Destroying the bullet when it collides with solid object
        {
            Instantiate(bulletParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

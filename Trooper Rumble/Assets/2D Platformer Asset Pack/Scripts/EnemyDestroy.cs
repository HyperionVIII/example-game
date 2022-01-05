using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    private UnityEngine.Object enemyParticles;
    private UnityEngine.Object enemyDamageParticles;
    public int health = 1;

    void Start()
    {
        enemyParticles = Resources.Load("EnemyDestroy");
        enemyDamageParticles = Resources.Load("EnemyDamage");
    }

    public void DamageEnemy()
    {
        health--;
        if(health <= 0)
        {
            Instantiate(enemyParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(enemyDamageParticles, transform.position, Quaternion.identity);
        }
      
    }
}

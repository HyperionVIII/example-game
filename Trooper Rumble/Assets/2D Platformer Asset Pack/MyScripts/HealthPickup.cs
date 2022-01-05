using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    Player playerHealth;

    public int healthBonus = 10;

    private void Awake()
    {
        playerHealth = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (playerHealth.currentHealth < playerHealth.maxHealth) 
        {
            Destroy(gameObject);
            playerHealth.currentHealth = playerHealth.currentHealth + healthBonus; 
        }
    }
}

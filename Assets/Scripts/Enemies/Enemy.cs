using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 5f;
    public Rigidbody enemyRb;

    public void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}

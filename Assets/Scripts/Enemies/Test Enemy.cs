using System.Runtime.CompilerServices;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    float health = 6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("bullet"))
        {
            health--;
        }   
    }
}

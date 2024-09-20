using System;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    float bulletSpeed = 50f;
    
    void Start()
    {
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        Destroy(this.gameObject, 10f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}

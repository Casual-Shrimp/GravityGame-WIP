using System;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private float gravity = -9.81f;
    private float bulletSpeed = 100f;
    private Rigidbody rb;
    private float drag = 0.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(bulletSpeed * Time.deltaTime, 0, 0));
    }

    private void FixedUpdate()
    {
        BulletTragectory();
    }

    void BulletTragectory()
    {
        rb.velocity = new Vector3((bulletSpeed * Time.deltaTime) - drag, 0, 0);
    }
}

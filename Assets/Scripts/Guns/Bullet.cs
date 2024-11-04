using System;
using Unity.VisualScripting;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    float _bulletSpeed = 60f;
    public GameObject Player;
    public Movement movement;
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        movement = Player.GetComponent<Movement>();
        if (movement.gravity > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        else if (movement.gravity < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
        Destroy(this.gameObject, 1.5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
    
}
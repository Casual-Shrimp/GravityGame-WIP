using UnityEngine;

public class Rifle : MonoBehaviour
{
    private float damage = 1f;
    public float range = 100f;
    private float delay = 0.1f;
    private float hasShot;
    public Camera fpsCam;

    private void Start()
    {
        fpsCam = Camera.main;
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0) && Time.time > hasShot) //checks if the player can shoot 
        {
            hasShot = Time.time + delay;
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        Vector3 fwd = fpsCam.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(fpsCam.transform.position, fwd, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy) //when you hit the enemy the ray is turned green
            {
                Debug.DrawRay(fpsCam.transform.position, fwd * range, Color.green, 1f);
                enemy.TakeDamage(damage);
            }
            else //when any other object is hit that is not the enemy the ray turns red
            {
                Debug.DrawRay(fpsCam.transform.position, fwd * range, Color.red, 1f);
            }
        }
        else //if void is hit the ray turns blue
        {
            Debug.DrawRay(fpsCam.transform.position, fwd * range, Color.blue, 1f);
        }
        
        if (hit.rigidbody != null) //adds a force to the hit rigidbody
        {
            hit.rigidbody.AddForce(-hit.normal * 100);
        }
    }
}
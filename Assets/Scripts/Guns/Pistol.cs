using System;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

public class Pistol : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;



    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        Vector3 fwd = fpsCam.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(fpsCam.transform.position, fwd, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy) //when you hit the enemy the ray is turned green
            {
                Debug.DrawRay(fpsCam.transform.position, fwd * range, Color.green, 1f);
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

     
    }
}

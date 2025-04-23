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
        Shoot();
    }

    public void Shoot()
    {
        RaycastHit hit;
        Vector3 fwd  = fpsCam.transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(fpsCam.transform.position, fwd, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Debug.DrawRay(fpsCam.transform.position, fwd * range, Color.green, 1f);

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Debug.Log($"Hit {hit.collider.name} on layer {LayerMask.LayerToName(hit.transform.gameObject.layer)}");
        }

     
    }
}

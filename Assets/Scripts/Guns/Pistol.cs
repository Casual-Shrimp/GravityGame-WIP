using System;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

public class Pistol : MonoBehaviour
{
    public float damage = 10f;
    public float range = 200;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot();
            Debug.Log("This ist the left mouse button");
        }
    }

    void Shoot()
    {

    }
}

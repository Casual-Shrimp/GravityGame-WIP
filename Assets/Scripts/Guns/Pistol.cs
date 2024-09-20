using System;
using UnityEngine;
using UnityEngine.Animations;

public class Pistol : MonoBehaviour
{
    public GameObject bullet;
    private float fireRate = 0.1f;
    private float currentTime;
    private float passedTime;
    public GameObject nozzle;

    private void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        if (Input.GetKeyDown(KeyCode.Mouse0) && passedTime < currentTime)
        {
            Instantiate(bullet, nozzle.transform.position, transform.rotation);
            passedTime = currentTime + fireRate;
        }
    }

}

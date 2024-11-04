using System;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

public class Pistol : MonoBehaviour
{
    public GameObject bullet;
    private float _fireRate = 0.25f;
    private float _currentTime;
    private float _passedTime;
    public GameObject nozzle;
    private float _x;
    private float _y;
    private float _z;

    private void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime = Time.time;
        Fire();
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _passedTime < _currentTime)
        {
            Instantiate(bullet, nozzle.transform.position, transform.rotation);
            _passedTime = _currentTime + _fireRate;
        }


    }

}

using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform weaponSlot;
    public GameObject weapon;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pistol"))
        {
            Debug.Log("Pistol detected");
            Instantiate(weapon, weaponSlot);
            var script = weapon.GetComponent<Pistol>();
            script.fpsCam = Camera.main;
            Destroy(other.gameObject);
        }
    }
}

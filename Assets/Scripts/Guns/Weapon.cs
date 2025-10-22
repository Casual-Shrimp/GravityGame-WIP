using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform weaponSlot;
    public GameObject weapon;

    [SerializeField] GameObject[] Arsenal = new GameObject[2];
    
    void Start()
    {
    }
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pistol"))
        {
            weapon = Arsenal[0];
            var script = weapon.GetComponent<Pistol>();
            Instantiate(weapon, weaponSlot);
            script.fpsCam = Camera.main;
            Destroy(other.gameObject);
        }
    }
}

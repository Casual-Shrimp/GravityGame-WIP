using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform weaponSlot;
    public GameObject weapon;
    public int slot = 0;
    public float changeDelay = 0.4f;
    public float changed;

    [SerializeField] GameObject[] Arsenal = new GameObject[5];
    
    void Start()
    {
    }
    void FixedUpdate()
    {
        WeaponSelect();
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pistol"))
        {
            weapon = Arsenal[slot];
            var script = weapon.GetComponent<Pistol>();
            Instantiate(weapon, weaponSlot);
            script.fpsCam = Camera.main;
            Destroy(other.gameObject);
        }
    }

    void WeaponSelect()
    {
        
    }
}

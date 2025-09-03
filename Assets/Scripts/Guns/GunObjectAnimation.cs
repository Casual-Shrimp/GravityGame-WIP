using JetBrains.Annotations;
using UnityEngine;

public class GunObjectAnimation : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 6.5f, 0));
    }
}

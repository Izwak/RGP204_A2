using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRb;

    public float damage;
    public Vector3 hitPoint;
    public float shootForce;
    // Start is called before the first frame update
    void Start()
    {
        // Moves the bullet
        this.GetComponent<Rigidbody>().AddForce((hitPoint - this.transform.position).normalized * shootForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Do the collisions
    }
}

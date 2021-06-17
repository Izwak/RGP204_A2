using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{
    public GameObject bullet; // The bullet to be instantiated
    public Transform attackPoint;
    public Camera playerCam;

    [Header("Gun Statistics")]
    public float timeBetweenSprays;
    public float fireRate;

    private bool shooting;
    private bool readyToShoot;

    private bool allowInvoke = true;

    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        // Player can hold left click to spray bullets
        shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(readyToShoot && shooting)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = true;

        // Ray going through the middle of the screen
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100);

        Quaternion fireRotation = Quaternion.LookRotation(transform.forward);

        // Spawn the bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.Euler(fireRotation.eulerAngles.x, fireRotation.eulerAngles.y, -90));
        currentBullet.GetComponent<Bullet>().hitPoint = targetPoint;

        // Destroy bullet after 2 sec
        Destroy(currentBullet.gameObject, 2.0f);

        if(allowInvoke)
        {
            Invoke("ResetShot", fireRate);
            allowInvoke = false;
        }        
    }

    private void ResetShot()
    {
        // Resets variables back to true
        readyToShoot = true;
        allowInvoke = true;
    }
}

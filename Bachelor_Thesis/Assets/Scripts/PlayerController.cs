using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public float width, height;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var xi = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
        var yi = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;

        Move(xi,yi); 


        var xc = CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * 100.0f;
        var yc = CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime * 100.0f;

        Move(xc, yc);

        CheckIfInPlayGround();

        var fire = CrossPlatformInputManager.GetAxis("Fire"); 

        if (Input.GetKeyDown(KeyCode.Space) || fire > 0f)
        {
            CmdFire();
        }

    }

    private void CheckIfInPlayGround()
    {
        if (transform.position.x < -width)
            transform.position = new Vector3(width, transform.position.y, transform.position.z);
        if (transform.position.x > width)
            transform.position = new Vector3(-width, transform.position.y, transform.position.z);
        if (transform.position.y < -height)
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        if (transform.position.y > height)
            transform.position = new Vector3(transform.position.x, -height, transform.position.z);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 6;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    void Move(float x, float y)
    {
        
        transform.Translate(x, y, 0);
    }
}

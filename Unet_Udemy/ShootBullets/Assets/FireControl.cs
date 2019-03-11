using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireControl : NetworkBehaviour
{

    public GameObject bulletPrefab;
    public GameObject spawnPosition;

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown("space"))
        {
            CmdShoot();
        }
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = spawnPosition.transform.forward * 50;
        Destroy(bullet, 4.0f);
    }

    [Command]
    private void CmdShoot()
    {
        CreateBullet();
        RpcCreateBullet();
    }

    [ClientRpc]
    private void RpcCreateBullet()
    {
        if (!isServer)
            CreateBullet();
    }
}

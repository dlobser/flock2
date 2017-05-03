using UnityEngine;
using UnityEngine.Networking;

public class GunItem : NetworkBehaviour, IUsable {
    public GameObject projectilePrefab;
    private Transform barrel;
    public float speed = 6f;

    void Start()
    {
        barrel = transform.FindChild("Barrel");
    }

	public void StartUsing(NetworkInstanceId handId)
    {
        var projectile = (GameObject)Instantiate(projectilePrefab, barrel.position, barrel.rotation);
        //projectile.GetComponent<Rigidbody>().AddForce(barrel.forward * speed, ForceMode.VelocityChange);  // is asynchronously and won't work here
        projectile.GetComponent<Rigidbody>().velocity = barrel.forward * speed;

        NetworkServer.Spawn(projectile);
    }
	public void StopUsing(NetworkInstanceId handId)
	{
	}
}

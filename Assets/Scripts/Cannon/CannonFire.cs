using cpeak.cPool;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
    [SerializeField] private cPool pool;
    [SerializeField] private string bulletPoolTag = "Bullet";
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 1f;
    private float lastFireTime;

    private void Update() 
    {
        if(Time.time > (1 / fireRate) + lastFireTime)
        {
            Fire();
            lastFireTime = Time.time;
        }    
    }

    private void Fire()
    {
        pool.GetPoolObject(bulletPoolTag, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}

using cpeak.cPool;
using DG.Tweening;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
    [SerializeField] private cPool pool;
    [SerializeField] private Health health;
    [SerializeField] private string bulletPoolTag = "Bullet";
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 1f;
    private float lastFireTime;

    [Header("Wider Shooting Settings")]
    private bool disperseBullets = false;
    [SerializeField] private int disperseBulletAmount = 3;
    [SerializeField] private float disperseDuration = .1f;

    public float GetFireRate => fireRate;
    public float SetFireRate {set { fireRate = value; } }

    private void Awake() 
    {
        health.OnDie.AddListener(HandleOnDie);
    }

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
        if(!disperseBullets)
        {
            pool.GetPoolObject(bulletPoolTag, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
        else
        {
            float distanceBtwBullets = .5f;
            int sideBullets = disperseBulletAmount / 2;

            if(disperseBulletAmount % 2 != 0)
            {
                GameObject bulletInstance = pool.GetPoolObject(bulletPoolTag, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }

            /// Left side bullets
            for (int i = 0; i < sideBullets; i++)
            {
                GameObject bulletInstance = pool.GetPoolObject(bulletPoolTag, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

                bulletInstance.transform.DOMoveX(distanceBtwBullets * (i + 1) * -1f, disperseDuration).SetRelative();
            }

            /// Right side bullets
            for (int i = 0; i < sideBullets; i++)
            {
                GameObject bulletInstance = pool.GetPoolObject(bulletPoolTag, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

                bulletInstance.transform.DOMoveX(distanceBtwBullets * (i + 1) , disperseDuration).SetRelative();
            }
            
        }
    }

    private void HandleOnDie()
    {
        this.enabled = false;
    }

    public void SetWiderShootingState(bool isActive)
    {
        disperseBullets = isActive;
    }
}

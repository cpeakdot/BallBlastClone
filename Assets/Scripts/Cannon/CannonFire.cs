using cpeak.cPool;
using DG.Tweening;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
    [SerializeField] private cPool pool;
    [SerializeField] private Health health;
    [SerializeField] private FasterShooting fasterShooting;
    [SerializeField] private WiderShooting widerShooting;
    [SerializeField] private string bulletPoolTag = "Bullet";
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float initialFireRate = 1f;
    private float fireRate = 1f;
    private float lastFireTime;

    [Header("Wider Shooting Settings")]
    [SerializeField] private int initialDisperseBulletAmount = 1;
    private int disperseBulletAmount = 1;
    [SerializeField] private float disperseDuration = .1f;

    public float GetFireRate => fireRate;
    public float SetFireRate {set { fireRate = value; } }

    private void Awake() 
    {
        health.OnDie.AddListener(HandleOnDie);
    }

    private void Start() 
    {
        AttributeManager.OnUpgrade += HandleOnUpgrade;

        initialFireRate += AttributeManager.Instance.GetShootingRateUpgradedAmount();

        initialDisperseBulletAmount += AttributeManager.Instance.GetWiderShootingCountUpgradedAmount();

        fireRate = initialFireRate;

        disperseBulletAmount = initialDisperseBulletAmount;
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
        float distanceBtwBullets = .5f;

        Vector3 centerPosition = bulletSpawnPoint.position;

        float totalWidth = (disperseBulletAmount - 1) * distanceBtwBullets;
        
        float startPosition = centerPosition.x - totalWidth / 2f;

        for (int i = 0; i < disperseBulletAmount; i++)
        {
            GameObject bulletInstance = pool.GetPoolObject(bulletPoolTag, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            float offsetX = i * distanceBtwBullets;

            float moveToXPosition = startPosition + offsetX;

            bulletInstance.transform.DOMoveX(moveToXPosition, disperseDuration);
        }
    }

    private void HandleOnDie()
    {
        this.enabled = false;
    }

    public void SetWiderShootingBulletAmount(int bulletAmount)
    {
        this.disperseBulletAmount = bulletAmount;
    }

    public int GetWiderShootingBulletAmount()
    {
        return initialDisperseBulletAmount;
    }

    private void HandleOnUpgrade(UpgradeType upgradeType)
    {
        if(upgradeType == UpgradeType.FasterShooting)
        {
            initialFireRate += AttributeManager.Instance.GetShootingRateUpgradedAmount();

            if(!fasterShooting.IsFasterShootingActive)
            {
                fireRate = initialFireRate;
            }
            else
            {
                fasterShooting.SetInitialFireRate = initialFireRate;
            }
        }
        else if(upgradeType == UpgradeType.WiderShooting)
        {
            initialDisperseBulletAmount += AttributeManager.Instance.GetWiderShootingCountUpgradedAmount();

            if(!widerShooting.IsWiderShootingActive)
            {
                disperseBulletAmount = initialDisperseBulletAmount;
            }
            else
            {
                widerShooting.SetInitialBulletAmount = initialDisperseBulletAmount;
                disperseBulletAmount++;
            }
        }
    }
}

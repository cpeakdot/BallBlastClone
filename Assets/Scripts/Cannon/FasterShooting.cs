using UnityEngine;

public class FasterShooting : CannonPowerBase
{
    [SerializeField] private CannonFire cannonFire;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float fireRateMultiplier = 2f;
    private float initialFireRate;
    private float timer = 0f;
    private bool isFasterShootingActive = false;

    public bool IsFasterShootingActive => isFasterShootingActive;
    public float SetInitialFireRate {set { initialFireRate = value; } }

    [ContextMenu("Test Faster Shooting")]
    public override void InitPower()
    {
        timer = 0f;

        if (isFasterShootingActive) { return; }

        isFasterShootingActive = true;

        initialFireRate = cannonFire.GetFireRate;

        cannonFire.SetFireRate = initialFireRate * fireRateMultiplier;

        ActivatePowerupVisual();
    }

    private void Update() 
    {
        if (!isFasterShootingActive) { return; }

        timer += Time.deltaTime;

        if(timer >= lifeTime)
        {
            isFasterShootingActive = false;

            cannonFire.SetFireRate = initialFireRate;
        }
    }

    public override void ActivatePowerupVisual()
    {
        GameManager.Instance.ActivatePowerupVisual(powerUpType, lifeTime);
    }
}

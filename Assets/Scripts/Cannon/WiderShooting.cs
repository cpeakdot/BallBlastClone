using UnityEngine;

public class WiderShooting : CannonPowerBase
{
    [SerializeField] private CannonFire cannonFire;
    [SerializeField] private float lifeTime = 10f;
    private float timer = 0f;
    private bool isWiderShootingActive = false;
    private int initialBulletAmount;

    public bool IsWiderShootingActive => isWiderShootingActive;
    public int SetInitialBulletAmount {set { initialBulletAmount = value; } }

    [ContextMenu("Test Wider Shooting")]
    public override void InitPower()
    {
        timer = 0f;

        isWiderShootingActive = true;

        initialBulletAmount = cannonFire.GetWiderShootingBulletAmount();

        cannonFire.SetWiderShootingBulletAmount(initialBulletAmount + 1);

        ActivatePowerupVisual();
    }

    private void Update() 
    {
        if (!isWiderShootingActive) { return; }

        timer += Time.deltaTime;

        if(timer >= lifeTime)
        {
            timer = 0f;
            isWiderShootingActive = false;
            cannonFire.SetWiderShootingBulletAmount(initialBulletAmount);
        }
    }

    public override void ActivatePowerupVisual()
    {
        GameManager.Instance.ActivatePowerupVisual(powerUpType, lifeTime);
    }
}

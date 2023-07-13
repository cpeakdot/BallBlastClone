using UnityEngine;

public class WiderShooting : CannonPowerBase
{
    [SerializeField] private CannonFire cannonFire;
    [SerializeField] private float lifeTime = 10f;
    private float timer = 0f;
    private bool isWiderShootingActive = false;

    [ContextMenu("Test Wider Shooting")]
    public override void InitPower()
    {
        timer = 0f;
        isWiderShootingActive = true;
        cannonFire.SetWiderShootingState(true);
    }

    private void Update() 
    {
        if (!isWiderShootingActive) { return; }

        timer += Time.deltaTime;

        if(timer >= lifeTime)
        {
            timer = 0f;
            isWiderShootingActive = false;
            cannonFire.SetWiderShootingState(false);
        }
    }
}

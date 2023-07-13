using UnityEngine;

public class Invincibility : CannonPowerBase
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject invincibilityVisual;
     [SerializeField] private float lifeTime = 10f;
    private float timer = 0f;
    private bool isInvincibilityActive = false;

    [ContextMenu("Test Invincibility")]

    public override void InitPower()
    {
        timer = 0f;
        isInvincibilityActive = true;
        invincibilityVisual.SetActive(true);
        health.SetCanBeDamaged(false);
    }

    private void Update() 
    {
        if (!isInvincibilityActive) { return; }

        timer += Time.deltaTime;

        if(timer >= lifeTime)
        {
            health.SetCanBeDamaged(true);
            isInvincibilityActive = false;
            timer = 0f;
            invincibilityVisual.SetActive(false);
        }
    }
}

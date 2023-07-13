using cpeak.cPool;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float travelSpeed = 3f;
    [SerializeField] private float lifeTime = 4f;
    private int initialDamageToDeal = 1;
    private int damageToDeal;
    [SerializeField] private string itemTagOnPool = "Bullet";
    [SerializeField] private string hitParticlePoolTag = "HitParticle";
    private float timer = 0f;

    private void OnEnable() 
    {
        initialDamageToDeal = 1 + AttributeManager.Instance.GetFirePowerUpgradedAmount();

        damageToDeal = initialDamageToDeal;

        timer = 0f;
    }

    private void Update() 
    {
        transform.position += Vector3.up * (travelSpeed * Time.deltaTime);

        timer += Time.deltaTime;

        if(timer >= lifeTime)
        {
            DestroyThisObject();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!other.CompareTag("Boulder")) { return; }
        if (!other.TryGetComponent(out Boulder boulder)) { return; }

        Health boulderHealth = boulder.GetHealthComponent;

        Color boulderColor = boulder.GetBoulderColor;

        GameObject hitParticleInstance = 
            cPool.instance.GetPoolObject(hitParticlePoolTag, transform.position, Quaternion.identity, true, 1f);

        if(hitParticleInstance.TryGetComponent(out ParticleColorSetter particleColorSetter))
        {
            particleColorSetter.UpdateColor(boulderColor);
        }

        boulderHealth.Damage(damageToDeal);

        DestroyThisObject();
    }

    private void DestroyThisObject()
    {
        cPool.instance.ReleaseObject(itemTagOnPool, this.gameObject);
    }
}

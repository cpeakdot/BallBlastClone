using UnityEngine;
using System;
using DG.Tweening;
using cpeak.cPool;
using EZCameraShake;

[System.Serializable]
public class Boulder : MonoBehaviour
{
    [SerializeField] private int boulderIndex = 0;
    [SerializeField] private Boulder smallerBoulderPrefab;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Health health;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float wallForce = 150f;
    [SerializeField] private string dustParticlePoolTag = "DustParticle";
    [SerializeField] private string deathParticlePoolTag = "BoulderDeathParticle";
    private Color boulderColor;
    private bool isPunchingScaleActive = false;
    private bool canBeSplitted = true;

    public Color GetBoulderColor => boulderColor;
    public Health GetHealthComponent => health;
    public Rigidbody GetRigidbody => rigidBody;
    public int GetBoulderIndex => boulderIndex;

    public static event Action<Boulder> OnBoulderCrack;

    private void Awake() 
    {
        boulderColor = meshRenderer.material.color;
    }

    private void OnEnable() 
    {
        health.OnHealthUpdate.AddListener(HandleOnHealthUpdate);
        health.OnDie.AddListener(HandleOnDie);
    }

    private void OnDisable() 
    {
        health.OnHealthUpdate.RemoveListener(HandleOnHealthUpdate);
        health.OnDie.RemoveListener(HandleOnDie);
    }

    private void HandleOnHealthUpdate(int health)
    {
        if (isPunchingScaleActive) { return; }
        isPunchingScaleActive = true;

        float punchDuration = .2f;
        float punchForce = .2f;
        Vector3 punch = new Vector3(1f, 1f, 0f) * punchForce;

        transform.DOPunchScale(punch, punchDuration)
        .OnComplete(() => { isPunchingScaleActive = false; });
    }

    private void HandleOnDie()
    {
        cPool pool = cPool.instance;

        if(smallerBoulderPrefab != null && canBeSplitted)
        {
            Vector3 leftBoulderPos = transform.position + new Vector3(-1f, 0f, 0f);
            Vector3 rightBoulderPos = transform.position + new Vector3(1f, 0f, 0f);

            Vector3 leftBoulderVelocity = new Vector3(-2f, 4f, 0f);
            Vector3 rightBoulderVelocity = new Vector3(2f, 4f, 0f);

            BoulderSpawner.Instance.SpawnBoulder(smallerBoulderPrefab, leftBoulderPos, leftBoulderVelocity);
            BoulderSpawner.Instance.SpawnBoulder(smallerBoulderPrefab, rightBoulderPos, rightBoulderVelocity);
        }

        CameraShaker.Instance.ShakeOnce(1f, 3f, .1f, .3f);

        OnBoulderCrack?.Invoke(this);

        pool.GetPoolObject(deathParticlePoolTag, transform.position, Quaternion.identity, true, 1f);

        pool.ReleaseObject("boulder_" + boulderIndex, this.gameObject);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            float xPos = transform.position.x;

            Vector3 force = wallForce * ((xPos > 0) ? Vector3.left : Vector3.right);

            rigidBody.AddForce(force);

            return;
        }   

        if(collision.gameObject.CompareTag("Ground"))
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce);

            Vector3 collisionContactPoint = collision.contacts[0].point;
            collisionContactPoint.z = -1f;

            cPool.instance.GetPoolObject(dustParticlePoolTag, collisionContactPoint, Quaternion.identity, true, 1f);

            CameraShaker.Instance.ShakeOnce(1f, 1f, .1f, .2f);

            return;
        }

        if (!collision.transform.TryGetComponent(out Cannon cannon)) { return; }
        
        cannon.GetHealth.Damage(1);
    }

    public void SetNoneSplittable()
    {
        canBeSplitted = false;
    }
}

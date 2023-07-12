using UnityEngine;
using DG.Tweening;
using cpeak.cPool;

public class Boulder : MonoBehaviour
{
    [SerializeField] private Boulder smallerBoulderPrefab;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Health health;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float wallForce = 150f;
    [SerializeField] private string dustParticlePoolTag = "DustParticle";
    private Color boulderColor;
    private bool isPunchingScaleActive = false;

    public Color GetBoulderColor => boulderColor;
    public Health GetHealthComponent => health;

    private void Awake() 
    {
        boulderColor = meshRenderer.material.color;
    }

    private void OnEnable() 
    {
        health.OnHealthUpdate.AddListener(HandleOnHealthUpdate);
    }

    private void OnDisable() 
    {
        health.OnHealthUpdate.RemoveListener(HandleOnHealthUpdate);
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

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            float xPos = transform.position.x;

            Vector3 force = wallForce * ((xPos > 0) ? Vector3.left : Vector3.right);

            rigidBody.AddForce(force);
        }   

        if(collision.gameObject.CompareTag("Ground"))
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce);

            Vector3 collisionContactPoint = collision.contacts[0].point;
            collisionContactPoint.z = -1f;

            cPool.instance.GetPoolObject(dustParticlePoolTag, collisionContactPoint, Quaternion.identity, true, 1f);
        } 
    }
}

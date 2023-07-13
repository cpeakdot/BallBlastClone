
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private SwerveInput swerveInput;
    [SerializeField] private Health health;
    [Header("Values")]
    [SerializeField] private float swerveSpeed = 1f;

    public float GetSwerveSpeed => swerveSpeed;

    private void Awake() 
    {
        health.OnDie.AddListener(HandleOnDie);
    }

    private void Update() 
    {
        transform.position += (Vector3.right * swerveInput.changeOnX) * (swerveSpeed * Time.deltaTime);
    }

    public void SetSwerveSpeed(float swerveSpeed)
    {
        this.swerveSpeed = swerveSpeed;
    }

    private void HandleOnDie()
    {
        this.enabled = false;
    }
}


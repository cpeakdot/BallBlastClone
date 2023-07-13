
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private SwerveInput swerveInput;
    [SerializeField] private Health health;

    [Header("Values")]
    [SerializeField] private float swerveSpeed = 1f;
    [SerializeField] private float maxXPos = 4.5f;

    public float GetSwerveSpeed => swerveSpeed;

    private void Awake() 
    {
        health.OnDie.AddListener(HandleOnDie);
    }

    private void Update() 
    {
        float targetX = Mathf.Clamp(
            transform.position.x + swerveInput.changeOnX * (swerveSpeed * Time.deltaTime)
            , -maxXPos
            , maxXPos);

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
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


using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject body, wreckedBody;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private SwerveInput swerveInput;
    [SerializeField] private Health health;
    [SerializeField] private CannonPowerBase[] powerups;
    [SerializeField] private float wheelRotationSpeed = 25f;

    public float GetWheelRotationSpeed => wheelRotationSpeed;
    public GameObject GetBodyOBJ => body;
    public Health GetHealth => health;

    private void Start() 
    {
        health.OnDie.AddListener(HandleOnDie);
    }

    private void Update() 
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].transform.Rotate(Vector3.forward * (swerveInput.changeOnX * Time.deltaTime * wheelRotationSpeed));
        }
    }

    private void HandleOnDie()
    {
        body.SetActive(false);
        
        wreckedBody.SetActive(true);

        GameManager.Instance.EndGame();

        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Powerup") && other.transform.TryGetComponent(out Powerup powerup))
        {
            for (int i = 0; i < powerups.Length; i++)
            {
                if (powerup.GetPowerupType != powerups[i].powerUpType) { continue; }

                powerups[i].InitPower();

                powerup.UsePower();

                break;
            }

            return;
        }

        if (!other.CompareTag("Coin")) { return; }
        if (!other.TryGetComponent(out Coin coin)) { return; }

        coin.Collect();
    }

    public void SetWheelRotationSpeed(float rotationSpeed)
    {
        wheelRotationSpeed = rotationSpeed;
    }
}

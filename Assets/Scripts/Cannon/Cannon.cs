using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject body, wreckedBody;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private SwerveInput swerveInput;
    [SerializeField] private Health health;
    [SerializeField] private float wheelRotationSpeed = 25f;

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
    }
}

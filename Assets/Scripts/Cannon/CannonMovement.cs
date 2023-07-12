
using UnityEngine;

namespace BallBlast.Cannon
{
    public class CannonMovement : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private SwerveInput swerveInput;
        [Header("Values")]
        [SerializeField] private float swerveSpeed = 1f;

        private void Update() 
        {
            transform.position += (Vector3.right * swerveInput.changeOnX) * (swerveSpeed * Time.deltaTime);
        }
    }
}


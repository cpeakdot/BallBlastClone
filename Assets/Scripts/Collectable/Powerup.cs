using UnityEngine;
using cpeak.cPool;

public class Powerup : MonoBehaviour
{
    [SerializeField] private PowerupType powerupType;
    [SerializeField] private string poolTag;
    public PowerupType GetPowerupType => powerupType;

    public void UsePower()
    {
        cPool.instance.ReleaseObject(poolTag, this.gameObject);
    }
}

using UnityEngine;

public abstract class CannonPowerBase : MonoBehaviour
{
    public PowerupType powerUpType;
    public abstract void InitPower();
    public abstract void ActivatePowerupVisual();
}

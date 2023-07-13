using UnityEngine;
using System;

public class AttributeManager : MonoBehaviour
{
    public static AttributeManager Instance { get; private set; }

    #region Upgrades

    private int fasterShootingUpgradeLevel;
    [SerializeField] private float fasterShootingUpgradeEffectPerLevel;

    private int widerShootingUpgradeLevel;
    [SerializeField] private int widerShootingUpgradeEffectPerLevel;

    private int firePowerUpgradeLevel;
    [SerializeField] private int firePowerUpgradeEffectPerLevel;

    #endregion

    public static event Action<UpgradeType> OnUpgrade;

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }    
        else
        {
            Destroy(this);
        }
    }

    public float GetShootingRateUpgradedAmount()
    {
        return fasterShootingUpgradeEffectPerLevel * fasterShootingUpgradeLevel;
    }

    public int GetWiderShootingCountUpgradedAmount()
    {
        return widerShootingUpgradeEffectPerLevel * widerShootingUpgradeLevel;
    }

    public int GetFirePowerUpgradedAmount()
    {
        return firePowerUpgradeEffectPerLevel * firePowerUpgradeLevel;
    }

    public int FireRateUpgradeLevel
    {
        set
        {
            fasterShootingUpgradeLevel = value;
            OnUpgrade?.Invoke(UpgradeType.FasterShooting);
        }
        get
        {
            return fasterShootingUpgradeLevel;
        }
    }

    public int WiderShootingUpgradeLevel
    {
        set
        {
            widerShootingUpgradeLevel = value;
            OnUpgrade?.Invoke(UpgradeType.WiderShooting);
        }
        get
        {
            return widerShootingUpgradeLevel;
        }
    }

    public int FirePowerUpgradeLevel
    {
        set
        {
            firePowerUpgradeLevel = value;
            OnUpgrade?.Invoke(UpgradeType.FirePower);
        }
        get
        {
            return firePowerUpgradeLevel;
        }
    }

    public float FireRateUpgradeEffect
    {
        get
        {
            return fasterShootingUpgradeEffectPerLevel;
        }
    }

    public int WiderShootingUpgradeEffect
    {
        get
        {
            return widerShootingUpgradeEffectPerLevel;
        }
    }

    public int FirePowerUpgradeEffect
    {
        get
        {
            return firePowerUpgradeEffectPerLevel;
        }
    }
    
}

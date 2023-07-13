using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UpgradeButton : MonoBehaviour, IPointerClickHandler
{
    AttributeManager attributeManager;
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private Image upgradeImage;
    [SerializeField] private Sprite upgradeSprite;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text effectText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private string upgradeName;
    private float effectOnUpgrade;
    private float currentEffect = 0;
    private int upgradeLevel = 0;
    [SerializeField] int upgradeCost;
    private bool isEffectInProgress = false;

    private void Start() 
    {
        attributeManager = AttributeManager.Instance;

        GetAttributes();

        upgradeImage.sprite = upgradeSprite;

        nameText.text = upgradeName;

        costText.text = "<sprite=0> " + upgradeCost;

        UpdateCard();
    }

    private void UpdateCard()
    {
        effectText.text = effectOnUpgrade * upgradeLevel + " > " + effectOnUpgrade * (upgradeLevel + 1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int cost = upgradeCost;
        
        if (!TryPurchaseUpgrade(cost)) { return; }

        UpgradeEffectOnButton();

        upgradeLevel++;

        UpdateCard();

        switch (upgradeType)
        {
            case UpgradeType.FasterShooting:
                UpgradeShootingSpeed();
                break;
            case UpgradeType.WiderShooting:
                UpgradeShootingRange();
                break;
            case UpgradeType.FirePower:
                UpgradeFirePower();
                break;
            default:
                break;
        }
    }

    private bool TryPurchaseUpgrade(int amount)
    {
        return GameManager.Instance.TrySpendMoney(amount);
    }

    private void UpgradeShootingSpeed()
    {
        attributeManager.FireRateUpgradeLevel = upgradeLevel;
    }

    private void UpgradeShootingRange()
    {
        attributeManager.WiderShootingUpgradeLevel = upgradeLevel;
    }

    private void UpgradeFirePower()
    {
        attributeManager.FirePowerUpgradeLevel = upgradeLevel;
    }

    private void GetAttributes()
    {
        switch (upgradeType)
        {
            case UpgradeType.FasterShooting:
                upgradeLevel = attributeManager.FireRateUpgradeLevel;
                effectOnUpgrade = attributeManager.FireRateUpgradeEffect;
                break;
            case UpgradeType.WiderShooting:
                upgradeLevel = attributeManager.WiderShootingUpgradeLevel;
                effectOnUpgrade = attributeManager.WiderShootingUpgradeEffect;
                break;
            case UpgradeType.FirePower:
                upgradeLevel = attributeManager.FirePowerUpgradeLevel;
                effectOnUpgrade = attributeManager.FirePowerUpgradeEffect;
                break;
            default:
                break;
        }
    }

    private void UpgradeEffectOnButton()
    {
        if (isEffectInProgress) { return; }
        
        isEffectInProgress = true;

        transform.DOPunchScale(Vector3.one * -.05f, .2f)
        .OnComplete(() =>
        {
            isEffectInProgress = false;
        });
    }
}

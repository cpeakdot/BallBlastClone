using UnityEngine;
using System;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameState gameState;
    [SerializeField] private int money;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private RectTransform moneyRectTransform;
    [SerializeField] private PowerupUI[] powerupVisuals;
    [SerializeField] private GameObject restartGameButton;

    [Header("Upgrades")]
    [SerializeField] private Transform upgradesTransform;
    [SerializeField] private float upgradesRestingYPosition = -700f;
    [SerializeField] private float upgradesTargetYPosition = 700f;
    [SerializeField] private float upgradesWindowMoveDuration = 2f;
    [SerializeField] private Ease upgradesWindowMovementEase;

    public static RectTransform GetMoneyRectTransform => Instance.moneyRectTransform;
    public static event Action OnGameEnded;

    private void Awake() 
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start() 
    {
        UpdateMoneyValue();
    }

    public void RestartGame()
    {
        upgradesTransform.DOLocalMoveY(upgradesRestingYPosition, upgradesWindowMoveDuration)
        .OnComplete(()=>{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    public void EndGame()
    {
        ES3AutoSaveMgr.Current.Save();

        if (gameState == GameState.Ended) { return; }

        gameState = GameState.Ended;

        OnGameEnded?.Invoke();

        Invoke(nameof(EnableUpgradeWindow), 1f);
    }

    private void EnableUpgradeWindow()
    {
        upgradesTransform.DOLocalMoveY(upgradesTargetYPosition, upgradesWindowMoveDuration)
        .SetEase(upgradesWindowMovementEase)
        .OnComplete(()=>{
            restartGameButton.SetActive(true);
        });
    }

    public void ActivatePowerupVisual(PowerupType type, float lifeTime)
    {
        for (int i = 0; i < powerupVisuals.Length; i++)
        {
            if (powerupVisuals[i].GetPowerupType != type) { continue; }

            powerupVisuals[i].gameObject.SetActive(true);

            powerupVisuals[i].InitPowerupVisual(lifeTime);
        }
    }

#region Money
    public bool TrySpendMoney(int amountToSpend)
    {
        if (!CanSpendMoney(amountToSpend)) { return false; }

        AddMoney(-amountToSpend);

        return true;
    }

    public bool CanSpendMoney(int amountToSpend)
    {
        return money >= amountToSpend;
    }

    public void AddMoney(int amountToAdd)
    {
        money += amountToAdd;
        UpdateMoneyValue();
    }

    private void UpdateMoneyValue()
    {
        string moneyStr = FormatCurrency(money);
        moneyText.text = "<sprite=0> " + moneyStr;
    }

    public string FormatCurrency(int amount)
    {
        string[] suffixes = { "", "K", "M", "B", "T" };
        int suffixIndex = 0;

        double formattedAmount = amount;

        while (formattedAmount >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            formattedAmount /= 1000;
            suffixIndex++;
        }

        string formattedString = formattedAmount.ToString("0.#");
        string suffix = suffixes[suffixIndex];

        return formattedString + suffix;
    }
#endregion
}

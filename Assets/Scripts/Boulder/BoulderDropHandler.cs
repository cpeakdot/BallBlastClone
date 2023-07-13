using UnityEngine;
using cpeak.cPool;

public class BoulderDropHandler : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private int minCoinDropAmount = 0;
    [SerializeField] private int maxCoinDropAmount = 1;
    [Range(0, 100), SerializeField] private int powerupDropChance;
    private const string coinPoolTag = "coin";
    [SerializeField] private string[] droppablePowerupPoolTags;

    private void Awake() 
    {
        health.OnDie.AddListener(HandleOnBoulderDie);
    }

    private void HandleOnBoulderDie()
    {
#region CoinDrop
        int coinAmountToDrop = UnityEngine.Random.Range(minCoinDropAmount, maxCoinDropAmount + 1);

        for (int i = 0; i < coinAmountToDrop; i++)
        {
            cPool.instance.GetPoolObject(coinPoolTag, transform.position, Quaternion.identity);
        }
#endregion

#region Powerup

        bool dropPowerUp = UnityEngine.Random.Range(0, 101) <= powerupDropChance;

        if (!dropPowerUp) { return; }

        int powerUpToDrop = UnityEngine.Random.Range(0, droppablePowerupPoolTags.Length);

        cPool.instance.GetPoolObject(droppablePowerupPoolTags[powerUpToDrop], transform.position, Quaternion.identity);

#endregion
    }

}

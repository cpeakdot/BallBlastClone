using UnityEngine;
using DG.Tweening;
using cpeak.cPool;

public class Coin : MonoBehaviour
{
    private bool canBeCollected = true;
    private const string poolTag = "coin";

    private void OnEnable() 
    {
        canBeCollected = true;
    }
    public void Collect()
    {
        if (!canBeCollected) { return; }

        canBeCollected = false;

        Debug.Log("Coin collected");

        cPool.instance.ReleaseObject(poolTag, this.gameObject);
    }
}

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

    [ContextMenu("Test Collect")]
    public void Collect()
    {
        if (!canBeCollected) { return; }

        canBeCollected = false;

        RectTransform moneyTextRectTransform = GameManager.GetMoneyRectTransform;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(moneyTextRectTransform.transform.position);

        worldPosition.z = transform.position.z;

        float moveDuration = 1f;

        transform.DOMove(worldPosition, moveDuration)
        .OnComplete(() =>
        {
            GameManager.Instance.AddMoney(1);
            cPool.instance.ReleaseObject(poolTag, this.gameObject);
        })
        .SetEase(Ease.InOutCubic);
    }
}

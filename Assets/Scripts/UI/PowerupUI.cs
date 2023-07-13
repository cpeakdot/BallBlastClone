using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
    [SerializeField] private PowerupType powerupType;
    [SerializeField] private Image image;
    private float lifeTime;
    private float timeLeft;

    public PowerupType GetPowerupType => powerupType;

    public void InitPowerupVisual(float lifeTime)
    {
        this.lifeTime = lifeTime;
        timeLeft = lifeTime;
    }

    private void Update() 
    {
        image.fillAmount = timeLeft / lifeTime;

        timeLeft = Mathf.Max(0, timeLeft - Time.deltaTime);

        if(timeLeft == 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable() 
    {
        image.fillAmount = 1;
    }

}

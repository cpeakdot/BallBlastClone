using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private TMP_Text healthText;
    public UnityEvent OnDie;
    public UnityEvent<int> OnHealthUpdate;

    public void Damage(int amount)
    {
        health = Mathf.Max(0, health - amount);

        OnHealthUpdate?.Invoke(health);

        UpdateHealthText();

        if(health == 0)
        {
            OnDie?.Invoke();
        }
    }

    private void UpdateHealthText()
    {
        if (!healthText) { return; }
        healthText.text = health.ToString();
    }
}

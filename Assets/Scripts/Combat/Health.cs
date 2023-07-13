using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private TMP_Text healthText;
    public UnityEvent OnDie;
    public UnityEvent<int> OnHealthUpdate;
    private int currentHealth;
    private bool canBeDamaged = true;

    private void OnEnable() 
    {
        currentHealth = health;
    }

    public void Damage(int amount)
    {
        if (!canBeDamaged) { return; }

        currentHealth = Mathf.Max(0, currentHealth - amount);

        OnHealthUpdate?.Invoke(currentHealth);

        UpdateHealthText();

        if(currentHealth == 0)
        {
            OnDie?.Invoke();
        }
    }

    private void UpdateHealthText()
    {
        if (!healthText) { return; }
        healthText.text = currentHealth.ToString();
    }

    public void SetCanBeDamaged(bool canBeDamaged)
    {
        this.canBeDamaged = canBeDamaged;
    }
}

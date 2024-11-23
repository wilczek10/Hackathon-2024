using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    [SerializeField] private Image healthBarFill;
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void UpdateHealth(float amount) {
        currentHealth +=amount;
        UpdateHealthBar();
    }
    private void UpdateHealthBar() {
        float targetFillAmount = currentHealth / maxHealth;
        healthBarFill.fillAmount = targetFillAmount;
    }


}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 200; // Maksymalne zdrowie gracza
    private int currentHealth; // Aktualne zdrowie gracza

    void Start()
    {
        // Ustaw pocz¹tkowe zdrowie na maksymalne
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        // Zmniejsz zdrowie gracza
        currentHealth -= damage;

        // Zaktualizuj UI zdrowia
        UpdateHealthUI();

        // SprawdŸ, czy gracz zgin¹³
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log($"Gracz otrzyma³ {damage} obra¿eñ. Zdrowie: {currentHealth}/{maxHealth}");
        }
    }

    public void Heal(int amount)
    {
        // Zwiêksz zdrowie gracza
        currentHealth += amount;

        // Nie przekraczaj maksymalnego zdrowia
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Zaktualizuj UI zdrowia
        UpdateHealthUI();

        Debug.Log($"Gracz odzyska³ {amount} zdrowia. Zdrowie: {currentHealth}/{maxHealth}");
    }

    private void Die()
    {
        SceneManager.LoadScene("koniec gry");
        // Wywo³aj akcjê œmierci gracza
        Debug.Log("Gracz zgin¹³!");
        Destroy(gameObject);
        // Dodaj logikê np. restart gry, animacja œmierci
    }

    private void UpdateHealthUI()
    {
        // Tutaj mo¿esz zaktualizowaæ UI (np. pasek zdrowia)
        // Przyk³ad: HealthBar.UpdateHealth(currentHealth, maxHealth);
    }
}

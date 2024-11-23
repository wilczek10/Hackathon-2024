using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 200; // Maksymalne zdrowie gracza
    private int currentHealth; // Aktualne zdrowie gracza

    void Start()
    {
        // Ustaw pocz�tkowe zdrowie na maksymalne
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        // Zmniejsz zdrowie gracza
        currentHealth -= damage;

        // Zaktualizuj UI zdrowia
        UpdateHealthUI();

        // Sprawd�, czy gracz zgin��
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log($"Gracz otrzyma� {damage} obra�e�. Zdrowie: {currentHealth}/{maxHealth}");
        }
    }

    public void Heal(int amount)
    {
        // Zwi�ksz zdrowie gracza
        currentHealth += amount;

        // Nie przekraczaj maksymalnego zdrowia
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Zaktualizuj UI zdrowia
        UpdateHealthUI();

        Debug.Log($"Gracz odzyska� {amount} zdrowia. Zdrowie: {currentHealth}/{maxHealth}");
    }

    private void Die()
    {
        SceneManager.LoadScene("koniec gry");
        // Wywo�aj akcj� �mierci gracza
        Debug.Log("Gracz zgin��!");
        Destroy(gameObject);
        // Dodaj logik� np. restart gry, animacja �mierci
    }

    private void UpdateHealthUI()
    {
        // Tutaj mo�esz zaktualizowa� UI (np. pasek zdrowia)
        // Przyk�ad: HealthBar.UpdateHealth(currentHealth, maxHealth);
    }
}

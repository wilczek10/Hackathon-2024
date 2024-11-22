using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar; // Obraz paska zdrowia
    public float healthAmount = 100f; // Początkowa ilość zdrowia

    void Update()
    {
        // Sprawdzanie, czy zdrowie spadło do zera i restart poziomu
        if (healthAmount <= 0)
        {
            // Poprawna metoda ładowania poziomu
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        // Jeśli gracz naciśnie klawisz Enter, odejmij zdrowie
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }
    }

    // Metoda odejmowania zdrowia
    public void TakeDamage(float damage)
    {
        healthAmount -= damage; // Odejmij zdrowie
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // Upewnij się, że zdrowie nie jest poniżej 0 ani powyżej 100
        healthBar.fillAmount = healthAmount / 100f; // Zaktualizuj pasek zdrowia
    }

    // Metoda leczenia
    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount; // Dodaj zdrowie
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // Upewnij się, że zdrowie nie przekracza 100
        healthBar.fillAmount = healthAmount / 100f; // Zaktualizuj pasek zdrowia
    }
}

using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth1 : MonoBehaviour
{
    public int maxHealth = 100;               // Maksymalne zdrowie gracza
    public int currentHealth;                 // Bieżące zdrowie gracza
    public GameObject healthCirclePrefab;     // Prefab kółka zdrowia
    public Transform healthContainer;         // Miejsce, gdzie będą przechowywane kółka zdrowia

    private GameObject[] healthCircles;       // Tablica kółek zdrowia

    void Start()
    {
        currentHealth = maxHealth;             // Na początku zdrowie gracza to maksymalne zdrowie
        CreateHealthCircles();                 // Tworzymy kółka zdrowia tylko raz
    }

    // Tworzenie kółek zdrowia
    private void CreateHealthCircles()
    {
        // Upewniamy się, że nie tworzymy kółek wielokrotnie
        foreach (Transform child in healthContainer)
        {
            Destroy(child.gameObject); // Usuwamy istniejące obiekty (jeśli istnieją)
        }

        healthCircles = new GameObject[maxHealth]; // Tablica na kółka zdrowia

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject healthCircle = Instantiate(healthCirclePrefab, healthContainer);  // Tworzenie kółka
            healthCircle.transform.localPosition = new Vector2(i * 35, 0); // Ustawienie pozycji kółka (możesz zmienić odstępy)
            healthCircles[i] = healthCircle; // Dodanie kółka do tablicy
        }
    }

    // Funkcja odbierania obrażeń
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Zmniejszamy zdrowie
        if (currentHealth < 0) currentHealth = 0;   // Jeśli zdrowie jest mniejsze niż 0, ustawiamy 0
        UpdateHealthCircles();      // Zaktualizuj kółka zdrowia
    }

    // Aktualizacja kółek zdrowia
    private void UpdateHealthCircles()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            Image fillImage = healthCircles[i].GetComponentInChildren<Image>(); // Pobierz obrazek wypełnienia
            if (i < currentHealth)
            {
                fillImage.fillAmount = 1f; // Wypełnienie na 100%
            }
            else
            {
                fillImage.fillAmount = 0f; // Wypełnienie na 0%
            }
        }
    }
}
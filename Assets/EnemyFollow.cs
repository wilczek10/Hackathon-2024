using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target; // Cel, którym będzie gracz
    public float speed;       // Prędkość poruszania
    public bool useTriggers;  // Ustaw na true, jeśli chcesz używać triggerów zamiast kolizji fizycznych

    void Start()
    {
        // Znajdź obiekt gracza za pomocą tagu
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (target != null)
        {
            // Poruszaj się tylko w osi Y w kierunku celu
            Vector2 newPosition = new Vector2(transform.position.x, 
                Mathf.MoveTowards(transform.position.y, target.position.y, speed * Time.deltaTime));
            transform.position = newPosition;
        }
    }

    // Funkcja wykrywania kolizji (dla Collider 2D)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!useTriggers)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Zderzenie z graczem!");
                // Możesz tu dodać efekt np. zadanie obrażeń
            }
        }
    }

    // Funkcja wykrywania wejścia w trigger (dla Trigger 2D)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (useTriggers)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Gracz wszedł w trigger!");
                // Dodaj zachowanie, jeśli obiekt dotknął gracza
            }
     
        }}
}
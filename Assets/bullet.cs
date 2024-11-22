using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;  // Prêdkoœæ pocisku
    public int damage = 10;    // Obra¿enia zadawane przez pocisk
    public float lifetime = 5f; // Czas ¿ycia pocisku
    private Transform target;  // Cel, do którego zmierza pocisk
    private Vector3 direction;

    void Start()
    {
        // Zniszcz pocisk po okreœlonym czasie
        Destroy(gameObject, lifetime);
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        direction = (target.position - transform.position).normalized;
    }

    void Update()
    {
        if (target != null)
        {
            // Oblicz kierunek do celu

            // Przesuwaj pocisk w kierunku celu
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Obróæ pocisk w stronê celu
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
        else
        {
            // Jeœli cel zosta³ zniszczony, pocisk nie bêdzie mia³ dok¹d lecieæ
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Zadaj obra¿enia graczowi
                playerHealth.TakeDamage(damage);
            }
            // Zniszcz pocisk po trafieniu
            Destroy(gameObject);
        }
    }
}

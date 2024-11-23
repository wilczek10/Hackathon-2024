using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;  // Pr�dko�� pocisku
    public int damage = 10;    // Obra�enia zadawane przez pocisk
    public float lifetime = 5f; // Czas �ycia pocisku
    private Transform target;  // Cel, do kt�rego zmierza pocisk
    private Vector3 direction;

    void Start()
    {
        // Zniszcz pocisk po okre�lonym czasie
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

            // Obr�� pocisk w stron� celu
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
        else
        {
            // Je�li cel zosta� zniszczony, pocisk nie b�dzie mia� dok�d lecie�
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Zadaj obra�enia graczowi
                playerHealth.TakeDamage(damage);
            }
            // Zniszcz pocisk po trafieniu
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;  // Pr�dko�� pocisku
    public int damage = 10;    // Obra�enia zadawane przez pocisk
    public float lifetime = 5f; // Czas �ycia pocisku
    private Transform target;  // Cel, do kt�rego zmierza pocisk

    void Start()
    {
        // Zniszcz pocisk po okre�lonym czasie
        Destroy(gameObject, lifetime);
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        if (target != null)
        {
            // Oblicz kierunek do celu
            Vector3 direction = (target.position - transform.position).normalized;

            // Przesuwaj pocisk w kierunku celu
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Obr�� pocisk w stron� celu
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // Je�li cel zosta� zniszczony, pocisk nie b�dzie mia� dok�d lecie�
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
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

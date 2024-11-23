using UnityEngine;

public class pocisk : MonoBehaviour
{
    public float speed = 10f;  // Pr�dko�� pocisku
    public int damage = 10;    // Obra�enia zadawane przez pocisk
    public float lifetime = 5f; // Czas �ycia pocisku
    private Vector3 direction;

    void Start()
    {
        // Zniszcz pocisk po okre�lonym czasie
        Destroy(gameObject, lifetime);
    }

    // Funkcja ustalaj�ca kierunek na podstawie pozycji kursora
    public void SetTarget(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
    }

    void Update()
    {
        // Przesuwaj pocisk w kierunku celu
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Obr�� pocisk w stron� celu
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bot"))
        {
            zyciebot zyciebot = other.GetComponent<zyciebot>();
            if (zyciebot != null)
            {
                // Zadaj obra�enia graczowi
                zyciebot.TakeDamage(damage);
            }

            zycieboss zycieboss = other.GetComponent<zycieboss>();
            if (zycieboss != null)
            {
                // Zadaj obra�enia graczowi
                zycieboss.TakeDamage(damage);
            }

            // Zniszcz pocisk po trafieniu
            Destroy(gameObject);
        }
    }
}

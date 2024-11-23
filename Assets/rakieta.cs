using UnityEngine;

public class rakieta : MonoBehaviour
{
    public float speed = 10f;  // Pr�dko�� pocisku
    public int damage = 10;    // Obra�enia zadawane przez pocisk
    public float lifetime = 5f; // Czas �ycia pocisku
    private Transform target;  // Cel, do kt�rego zmierza pocisk
    private Vector3 direction;
    private Animator animator;
    bool moving = true;
    void Start()
    {
        // Zniszcz pocisk po okre�lonym czasie
        Destroy(gameObject, lifetime);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
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
            if (moving)
                transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Obr�� pocisk w stron� celu
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
        else
        {
            // Je�li cel zosta� zniszczony, pocisk nie b�dzie mia� dok�d lecie�
            animator.SetTrigger("wybuch");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Zadaj obra�enia graczowi
                playerHealth.TakeDamage(damage);
            }
            moving = false;
            animator.SetTrigger("wybuch");

            // Zniszcz pocisk po trafieniu
            //Destroy(gameObject);
        }
    }
    public void destory()
    {
        Destroy(gameObject);
    }
}

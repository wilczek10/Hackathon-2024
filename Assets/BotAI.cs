using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BotAI2D : MonoBehaviour
{
    public float speed = 3.0f;           // Pr�dko�� poruszania bota
    public float attackRange = 1.5f;    // Zasi�g ataku
    public int attackDamage = 10;       // Obra�enia zadawane przez bota

    private Transform player;           // Referencja do gracza
    public PlayerHealth playerHealth;  // Referencja do skryptu �ycia gracza
    private Animator animator;          // Animator bota
    private Rigidbody2D rb;             // Rigidbody 2D bota
    private float lastAttackTime;       // Czas ostatniego ataku
    private Vector2 movement;           // Kierunek ruchu
    public float cooldown = 10f; // Odst�p czasu mi�dzy strza�ami
    private float fireTimer;

    void Start()
    {
        fireTimer = cooldown;
        // Znajd� gracza po tagu
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("Nie znaleziono skryptu PlayerHealth na obiekcie gracza!");
            }
        }
        else
        {
            Debug.LogError("Nie znaleziono obiektu z tagiem 'Player'!");
        }

        // Pobierz komponenty
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Brak komponentu Rigidbody2D na obiekcie bota!");
        }

        if (animator == null)
        {
            Debug.LogError("Brak komponentu Animator na obiekcie bota!");
        }
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (player == null) return; // Je�li brak gracza, nic nie r�b

        // Oblicz odleg�o�� do gracza
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            // Poruszaj si� w kierunku gracza
            MoveTowardsPlayer();
        }
        else
        {
            // Je�li atak nie jest na cooldownie, wykonaj go
            
            
                if (fireTimer >= cooldown)
                {
                    AttackPlayer();
                    fireTimer -= cooldown;
                }
                
            
            else
            {
                // Zatrzymaj ruch podczas cooldownu
                movement = Vector2.zero;
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Ustaw animacj� poruszania
        if (animator != null)
        {
            animator.Play("poruszanie_bot");
        }

        // Oblicz kierunek ruchu w stron� gracza
        Vector2 direction = (player.position - transform.position).normalized;

        // Poruszaj si� tylko w poziomie
        movement = new Vector2(direction.x, 0);

        // Obr�� bota w kierunku gracza (tylko w poziomie)
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(-6, transform.localScale.y, transform.localScale.z); // Patrz w prawo
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(6, transform.localScale.y, transform.localScale.z); // Patrz w lewo
        }
    }

    void AttackPlayer()
    {
        // Ustaw animacj� ataku
        if (animator != null)
        {
            animator.Play("atak");
        }

        // Zadaj obra�enia graczowi, je�li referencja do PlayerHealth istnieje
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Bot zadaje " + attackDamage + " obra�e� graczowi.");
        }
        else
        {
            Debug.LogError("Nie mo�na zada� obra�e�, brak referencji do PlayerHealth!");
        }

        // Zaktualizuj czas ostatniego ataku
        lastAttackTime = Time.time;

        Debug.Log("Bot wykona� atak!");
    }

    void FixedUpdate()
    {
        // Poruszaj bota tylko w FixedUpdate
        if (movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }
}

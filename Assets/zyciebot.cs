using UnityEngine;

public class zyciebot : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("smiercbota");
            animator.Play("smierc");
            animator.SetTrigger("smierc");
            //Die();
        }
    }
    public void destory()
    {
        Destroy(gameObject);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

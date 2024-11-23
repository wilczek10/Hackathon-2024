using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Prędkość poruszania się
    public float jumpForce = 7f; // Siła skoku
    public Transform groundCheck; // Punkt sprawdzający, czy postać jest na ziemi
    public LayerMask groundLayer; // Warstwa "ziemi", która pozwala sprawdzić, czy gracz jest na ziemi

    private Rigidbody2D rb;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f; // Promień sprawdzania, czy postać dotyka ziemi
    private float moveInput;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Pobranie komponentu Rigidbody2D
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Sprawdzenie, czy postać dotyka ziemi
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Pobranie wejścia gracza (lewo/prawo)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Poruszanie postacią
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y); // Używamy linearVelocity

        // Obrót postaci w zależności od kierunku
        if (moveInput > 0)
        {
            if (GetCurrentClipName() == "trzymaniebroni" || GetCurrentClipName() == "bieganiezbronia")
            {
                transform.localScale = new Vector3(6, 6, 6);
                animator.Play("bieganiezbronia");
            }
            else
            {
                transform.localScale = new Vector3(6, 6, 6);
                animator.Play("bieganie");
            }
        }
        else if (moveInput < 0)
        {
            if (GetCurrentClipName() == "trzymaniebroni" || GetCurrentClipName() == "bieganiezbronia")
            {
                transform.localScale = new Vector3(-6, 6, 6);
                animator.Play("bieganiezbronia");
            }
            else
            {
                transform.localScale = new Vector3(-6, 6, 6);
                animator.Play("bieganie");
            }
        }
       

        // Skakanie, jeśli postać jest na ziemi i nacisniesz strzałkę w górę
        if (isGrounded && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Nadanie siły skoku
        }

        // Animacja "wyciąganie", gdy naciśniesz "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GetCurrentClipName() != "trzymaniebroni" && GetCurrentClipName() != "bieganiezbronia")
            {
                animator.Play("wyciaganie");
            }
            else if (GetCurrentClipName() == "trzymaniebroni" || GetCurrentClipName() == "bieganiezbronia")
            {
                animator.Play("chowanie");
            }

        }
    }

    private string GetCurrentClipName()
    {
        if(animator.GetCurrentAnimatorClipInfoCount(0) == 0)
        {
            Debug.LogWarning("No animation playing");
            return "e";
        }

        return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }
}

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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Pobranie komponentu Rigidbody2D
    }

    private void Update()
    {
        // Sprawdzenie, czy postać dotyka ziemi
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Pobranie wejścia gracza (lewo/prawo)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Poruszanie postacią
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Skakanie, jeśli postać jest na ziemi i nacisniesz strzałkę w górę
        if (isGrounded && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Nadanie siły skoku
        }
    }
}

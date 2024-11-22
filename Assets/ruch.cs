using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Pr�dko�� poruszania si�
    public float jumpForce = 7f; // Si�a skoku
    public Transform groundCheck; // Punkt sprawdzaj�cy, czy posta� jest na ziemi
    public LayerMask groundLayer; // Warstwa "ziemi", kt�ra pozwala sprawdzi�, czy gracz jest na ziemi

    private Rigidbody2D rb;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f; // Promie� sprawdzania, czy posta� dotyka ziemi
    private float moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Pobranie komponentu Rigidbody2D
    }

    private void Update()
    {
        // Sprawdzenie, czy posta� dotyka ziemi
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Pobranie wej�cia gracza (lewo/prawo)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Poruszanie postaci�
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Skakanie, je�li posta� jest na ziemi i nacisniesz strza�k� w g�r�
        if (isGrounded && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Nadanie si�y skoku
        }
    }
}

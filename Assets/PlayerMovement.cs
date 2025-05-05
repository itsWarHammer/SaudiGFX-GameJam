using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    public float moveSpeed = 6f;     // horizontal speed
    public float jumpForce = 12f;    // impulse applied upward
    public Transform groundCheck;      // empty child at the player�s feet
    public float groundRadius = 0.15f; // size of ground probe
    public LayerMask groundLayer;      // layer(s) that count as ground

    Rigidbody2D rb;
    bool isGrounded;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        /* --------- HORIZONTAL MOVE --------- */
        float dir = 0f;                 // default: idle
        if (Input.GetKey(KeyCode.A)) dir = -1f;
        if (Input.GetKey(KeyCode.D)) dir = 1f;
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);

        /* --------- GROUND CHECK ------------ */
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, groundRadius, groundLayer);

        /* --------- JUMP -------------------- */
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);     // cancel down?speed
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /* Draw probe in Scene view (optional) */
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}

using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float jumpForce = 12f;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb;
    SpriteRenderer sr;

    bool isGrounded, jumpQueued;
    float horizInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();          // grab the renderer
    }

    /* ---------- INPUT ---------- */
    void Update()
    {
        /* Horizontal input (+1 / 0 / –1) */
        horizInput = Input.GetAxisRaw("Horizontal");

        /* Flip sprite to face direction */
        if (horizInput != 0)
            sr.flipX = horizInput < 0;

        /* Jump input (queued for FixedUpdate) */
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
            jumpQueued = true;
    }

    /* ---------- PHYSICS ---------- */
    void FixedUpdate()
    {
        /* Ground probe */
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, groundRadius, groundLayer);

        /* Horizontal motion */
        rb.linearVelocity = new Vector2(horizInput * moveSpeed, rb.linearVelocity.y);

        /* Jump */
        if (jumpQueued)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);   // reset vertical speed
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpQueued = false;
        }
    }

    /* Optional gizmo */
    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    private string GetDebuggerDisplay() => ToString();
}

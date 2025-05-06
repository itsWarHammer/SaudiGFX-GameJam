using UnityEngine;

public class KickScript : MonoBehaviour
{
    public Transform leftmostPosition;      // Where to land (left side of map)
    public float kickForce = 20f;           // How strong the kick is
    public float knockbackDuration = 3.5f;  // How long control is disabled

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            PlayerMovement playerCtrl = collision.GetComponent<PlayerMovement>(); // Replace with your movement script

            if (playerRb != null)
            {
                // OPTIONAL: disable control
                if (playerCtrl != null)
                    playerCtrl.enabled = false;

                // Reset velocity before applying force
                playerRb.linearVelocity = Vector2.zero;

                // Apply a strong force to the left and upward
                Vector2 kickDirection = new Vector2(-1, 0).normalized;
                playerRb.AddForce(kickDirection * kickForce, ForceMode2D.Impulse);

                // Re-enable control after delay
                collision.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(ReEnableControl(playerCtrl));

                // OPTIONAL: After 1 second, snap to the leftmost position (as if they were sent flying across)
                //collision.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(SnapToLeft(playerRb.transform));
            }
        }
    }

    private System.Collections.IEnumerator ReEnableControl(MonoBehaviour playerCtrl)
    {
        yield return new WaitForSeconds(knockbackDuration);
        if (playerCtrl != null)
            playerCtrl.enabled = true;
    }

    private System.Collections.IEnumerator SnapToLeft(Transform player)
    {
        yield return new WaitForSeconds(1.0f); // Wait for the flying effect
        player.position = leftmostPosition.position;
    }
}

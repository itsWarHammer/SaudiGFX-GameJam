using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TouchTrigger : MonoBehaviour
{
    private void Awake()
    {
        // ensure this collider is a trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StackManager.Instance.AddToStack(gameObject);
    }
}

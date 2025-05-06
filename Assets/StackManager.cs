using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public static StackManager Instance { get; private set; }

    [Tooltip("How far above the player each new item sits")]
    public float yOffset = 1.0f;

    private Transform player;
    private List<GameObject> stacked = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
            Debug.LogError("StackManager: could not find a GameObject tagged Player!");
    }

    /// <summary>
    /// Call this when an object is touched.
    /// </summary>
    public void AddToStack(GameObject obj)
    {
        if (stacked.Contains(obj)) return;

        // 1) disable gravity & stop movement
        var rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;  // optional, makes it fully driven by transform
        }

        // 1b) disable all colliders so it no longer interacts
        foreach (var col in obj.GetComponents<Collider2D>())
        {
            col.enabled = false;
        }

        // 2) compute the world-space target position
        float height = yOffset * (stacked.Count + 1);
        Vector3 worldTarget = player.position + Vector3.up * height;

        // 3) reparent WITHOUT changing world transform, then snap position
        obj.transform.SetParent(player, worldPositionStays: true);
        obj.transform.position = worldTarget;

        stacked.Add(obj);
    }
}

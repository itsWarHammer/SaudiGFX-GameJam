using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 5;
    void Start() {
        
    }

    void decreaseHealth()
    {
        if (health > 1) {
            health--;
        } else {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update() {
    }
}

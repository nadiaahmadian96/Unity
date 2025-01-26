using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject[] fruits; // Array to hold different fruit prefabs
    private int remainingHits;  // Number of hits before the box disappears

    void Start()
    {
        // Set a random number of hits between 1 and 4
        remainingHits = Random.Range(1, 5);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is from the player hitting the box from below
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].normal.y < 0)
        {
            SpawnRandomFruit(); // Spawn a random fruit
            HandleBoxHit();     // Decrease hits and remove the box if needed
        }
    }

    void SpawnRandomFruit()
    {
        // Choose a random fruit from the array
        int randomIndex = Random.Range(0, fruits.Length);
        GameObject fruitPrefab = fruits[randomIndex];

        // Spawn the fruit slightly above the box
        Vector3 spawnPosition = transform.position + Vector3.up * 0.5f;
        Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }

    void HandleBoxHit()
    {
        // Decrease the number of hits remaining
        remainingHits--;

        // Destroy the box if no hits are left
        if (remainingHits <= 0)
        {
            Destroy(gameObject);
        }
    }
}

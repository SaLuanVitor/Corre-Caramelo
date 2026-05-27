using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelController.ColetarOsso(); // Avisa o score
            Destroy(gameObject); // Some com o osso
        }
    }
}
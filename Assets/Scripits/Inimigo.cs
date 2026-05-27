using UnityEngine;

public class Inimigo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelController.Morreu(); // Chama a derrota imediata
            Debug.Log("Game Over!");
        }
    }
}
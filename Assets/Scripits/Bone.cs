using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Esse é o alarme! Ele vai escrever no Console quem encostou no osso.
        Debug.Log("O petisco sentiu um toque de: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag);

        if (collision.CompareTag("Player"))
        {
            LevelController.ColetarOsso(); 
            Destroy(gameObject); 
        }
    }
}
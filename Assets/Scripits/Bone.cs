using UnityEngine;

public class OssoColetavel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o Player encostou no osso
        if (collision.CompareTag("Player"))
        {
            // Avisa o controller que um osso foi pego
            LevelController.ColetarOsso();
            
            // Some com o osso
            Destroy(gameObject);
        }
    }
}
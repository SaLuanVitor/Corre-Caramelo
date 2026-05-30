using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public AudioClip somDeColeta;
    
    // Isso cria uma barrinha de volume no Unity que vai de 0 a 1!
    [Range(0f, 1f)] 
    public float volumeDoSom = 0.5f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (somDeColeta != null)
            {
                // Agora ele usa a variável do volume em vez do "1f" fixo
                AudioSource.PlayClipAtPoint(somDeColeta, Camera.main.transform.position, volumeDoSom); 
            }

            LevelController.ColetarOsso(); 
            Destroy(gameObject); 
        }
    }
}
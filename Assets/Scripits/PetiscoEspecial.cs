using UnityEngine;

public class PetiscoEspecial : MonoBehaviour
{
    public float bonusDeVelocidade = 5f; 
    public float tempoDeEfeito = 4f; // Quantos segundos o efeito dura!
    
    public AudioClip somDeColeta;
    [Range(0f, 1f)] 
    public float volumeDoSom = 0.8f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (somDeColeta != null)
            {
                AudioSource.PlayClipAtPoint(somDeColeta, Camera.main.transform.position, volumeDoSom); 
            }

            Player scriptPlayer = collision.GetComponent<Player>();
            if (scriptPlayer != null)
            {
                // Agora enviamos o bônus E o tempo que ele dura
                scriptPlayer.AtivarBonusVelocidade(bonusDeVelocidade, tempoDeEfeito);
            }

            Destroy(gameObject); 
        }
    }
}
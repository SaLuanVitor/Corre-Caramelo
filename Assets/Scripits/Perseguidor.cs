using UnityEngine;

public class Perseguidor : MonoBehaviour
{
    public float velocidade = 2.5f; 
    private Transform player;

    void Start()
    {
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objPlayer != null) player = objPlayer.transform;
    }

    void Update()
    {
        // Só move se o jogo não estiver pausado (GameOver/Win)
        if (player != null && Time.timeScale > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocidade * Time.deltaTime);
            
            // Inverte o sprite baseado na direção
            if (player.position.x < transform.position.x)
                transform.localScale = new Vector3(1, 1, 1); 
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
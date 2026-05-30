using UnityEngine;

public class Perseguidor : MonoBehaviour
{
    public float velocidade = 2.5f; 
    private Transform player;
    private Rigidbody2D rb2d; // Chama a física do inimigo

    void Start()
    {
        // Acha o jogador
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objPlayer != null) player = objPlayer.transform;

        // Pega o componente de física do próprio inimigo
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Usamos FixedUpdate no lugar do Update sempre que vamos mexer com física pesada (batidas)
    void FixedUpdate()
    {
        if (player != null && Time.timeScale > 0)
        {
            // Calcula a direção que o inimigo deve ir
            Vector2 direcao = (player.position - transform.position).normalized;
            
            // Move o inimigo usando a FÍSICA (Isso impede que eles entrem um no outro!)
            rb2d.MovePosition(rb2d.position + direcao * velocidade * Time.fixedDeltaTime);
            
            // Inverte o desenho para olhar para o lado certo
            if (player.position.x < transform.position.x)
                transform.localScale = new Vector3(1, 1, 1); 
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
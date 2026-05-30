using UnityEngine;

public class Perseguidor : MonoBehaviour
{
    public float velocidade = 2.5f; 
    public float tamanhoDoInimigo = 2f; 

    [Header("Inteligência Artificial")]
    public float raioDeVisao = 1.5f; // O tamanho da "bolha" ao redor dele
    public float forcaDeSeparacao = 1.5f; // A força com que ele empurra os amigos pro lado

    private Transform player;
    private Rigidbody2D rb2d; 

    void Start()
    {
        // Acha o jogador
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objPlayer != null) player = objPlayer.transform;

        rb2d = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(tamanhoDoInimigo, tamanhoDoInimigo, 1f);
    }

    void FixedUpdate()
    {
        if (player != null && Time.timeScale > 0)
        {
            // 1. Instinto básico: ir para o cachorro
            Vector2 direcaoAoPlayer = (player.position - transform.position).normalized;

            // 2. Inteligência: Olhar em volta para não bater nos amigos
            Vector2 forcaDeFuga = Vector2.zero;
            
            // Cria um círculo imaginário para ver quem está perto
            Collider2D[] vizinhos = Physics2D.OverlapCircleAll(transform.position, raioDeVisao);

            foreach (Collider2D vizinho in vizinhos)
            {
                // Se o que ele viu for outro inimigo (tem o mesmo script) e não for ele mesmo...
                if (vizinho.gameObject != gameObject && vizinho.GetComponent<Perseguidor>() != null)
                {
                    // Descobre para que lado o amigo está e calcula a direção contrária
                    Vector2 distancia = transform.position - vizinho.transform.position;
                    
                    // Quanto mais perto o amigo estiver, mais forte é o empurrão para o lado
                    forcaDeFuga += distancia.normalized / distancia.magnitude; 
                }
            }

            // 3. Junta as duas vontades: ir pro cachorro + desviar dos amigos
            Vector2 direcaoFinal = (direcaoAoPlayer + (forcaDeFuga * forcaDeSeparacao)).normalized;

            // 4. Move o inimigo com a nova direção inteligente
            rb2d.MovePosition(rb2d.position + direcaoFinal * velocidade * Time.fixedDeltaTime);
            
            // 5. Inverte o visual para olhar pro lado certo
            Vector3 escalaAtual = transform.localScale;
            if (player.position.x < transform.position.x)
                escalaAtual.x = Mathf.Abs(escalaAtual.x); 
            else
                escalaAtual.x = -Mathf.Abs(escalaAtual.x);
                
            transform.localScale = escalaAtual;
        }
    }
}
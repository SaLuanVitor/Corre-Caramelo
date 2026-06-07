using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 10f; 
    private Vector2 movement; // Guarda o movimento do teclado/controle
    private Rigidbody2D rb2d;
    private Animator anim;

    private float xLimit = 22f; 
    private float yLimit = 13f; 

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (rb2d != null)
        {
            rb2d.gravityScale = 0; 
            rb2d.freezeRotation = true; 
        }
    }

    private void FixedUpdate()
    {
        // 1. Pega o movimento do controle/teclado primeiro
        Vector2 movimentoFinal = movement;

        // 2. Se o controle estiver parado, ele lê o Joystick da tela do celular!
        if (movimentoFinal == Vector2.zero)
        {
            // Transforma o toque do joystick em movimento cravado (para cima, baixo, lados)
            float moveX = JoystickVirtual.VetorInput.x > 0.2f ? 1 : (JoystickVirtual.VetorInput.x < -0.2f ? -1 : 0);
            float moveY = JoystickVirtual.VetorInput.y > 0.2f ? 1 : (JoystickVirtual.VetorInput.y < -0.2f ? -1 : 0);
            
            movimentoFinal = new Vector2(moveX, moveY).normalized; 
        }

        // 3. Aplica o movimento final no Caramelo
        rb2d.linearVelocity = movimentoFinal * speed;

        // 4. Limita para não sair da tela
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);
        transform.position = pos;

        // 5. Animação de correr
        if (anim != null)
        {
            bool andando = movimentoFinal.magnitude > 0.01f;
            anim.SetBool("isRunning", andando);
        }

        // 6. Vira o cachorro para o lado certo
        if (movimentoFinal.x > 0.1f) transform.localScale = new Vector3(1, 1, 1);
        else if (movimentoFinal.x < -0.1f) transform.localScale = new Vector3(-1, 1, 1);
    }

    public void OnMove(InputValue inputValue)
    {
        movement = inputValue.Get<Vector2>();
    }
    
    public void AumentarVelocidade(float bonus)
    {
        speed += bonus;
    }

    public void AtivarBonusVelocidade(float bonus, float tempo)
    {
        StartCoroutine(RotinaVelocidade(bonus, tempo));
    }

    private System.Collections.IEnumerator RotinaVelocidade(float bonus, float tempo)
    {
        speed += bonus; 
        yield return new WaitForSeconds(tempo); 
        speed -= bonus; 
    }
}
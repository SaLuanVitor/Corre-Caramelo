using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 10f; 
    private Vector2 movement; 
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
        // Agora o movimento depende APENAS do teclado ou do controle físico
        Vector2 movimentoFinal = movement;

        rb2d.linearVelocity = movimentoFinal * speed;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);
        transform.position = pos;

        if (anim != null)
        {
            bool andando = movimentoFinal.magnitude > 0.01f;
            anim.SetBool("isRunning", andando);
        }

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
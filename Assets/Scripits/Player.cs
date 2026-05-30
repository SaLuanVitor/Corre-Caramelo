// Nome do arquivo: Player.cs
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
        rb2d.linearVelocity = movement * speed;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);
        transform.position = pos;

        if (anim != null)
        {
            bool andando = movement.magnitude > 0.01f;
            anim.SetBool("isRunning", andando);
        }

        if (movement.x > 0.1f) transform.localScale = new Vector3(1, 1, 1);
        else if (movement.x < -0.1f) transform.localScale = new Vector3(-1, 1, 1);
    }

    public void OnMove(InputValue inputValue)
    {
        movement = inputValue.Get<Vector2>();
    }
    
    // Função nova: Recebe o bônus e soma na velocidade atual
    public void AumentarVelocidade(float bonus)
    {
        speed += bonus;
    }

    // Essa biblioteca precisa estar declarada lá na primeira linha do script, 
    // mas se já tiver "using System.Collections;", não se preocupe.

    public void AtivarBonusVelocidade(float bonus, float tempo)
    {
        // Inicia o cronômetro
        StartCoroutine(RotinaVelocidade(bonus, tempo));
    }

    private System.Collections.IEnumerator RotinaVelocidade(float bonus, float tempo)
    {
        // 1. Aumenta a velocidade
        speed += bonus; 
        
        // 2. Espera os segundos que você escolher
        yield return new WaitForSeconds(tempo); 
        
        // 3. Tira o bônus, voltando ao normal
        speed -= bonus; 
    }
}
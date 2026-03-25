using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 10f; // Aumentei um pouco a velocidade para o mapa grande
    private Vector2 movement;
    private Rigidbody2D rb2d;
    private Animator anim;

    // NOVOS LIMITES: Ajustados para o tamanho da sua grama (Size 15 da Camera)
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

        // Agora o cachorro consegue explorar todo o cenário de grama
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Petisco"))
        {
            other.gameObject.SetActive(false);
            LevelController.ColetarOsso();
            Debug.Log("Petisco coletado!");
        }
    }
}
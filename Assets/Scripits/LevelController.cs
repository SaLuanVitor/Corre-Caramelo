using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour 
{
    public static LevelController instance; 
    
    [Header("Configurações de UI")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public GameObject win;
    public GameObject gameOver; 

    [Header("Configurações do Jogo")]
    public int tempoTotal = 60;
    public int objetivoDeOssos = 10; // AJUSTADO PARA 10

    private float timer;
    private int ossosColetados;
    private bool isGameOver;

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        timer = tempoTotal;
        Time.timeScale = 1; // Garante que o jogo comece rodando
        AtualizarUI();
    }
    
    private void Update()
    {
        if (isGameOver) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timerText != null)
                timerText.text = "Tempo: " + Mathf.Ceil(timer).ToString();
        }
        else
        {
            FinalizarJogo(false);
        }
    }

    public static void ColetarOsso()
    {
        if (instance != null && !instance.isGameOver)
        {
            instance.ossosColetados++;
            instance.AtualizarUI();

            if (instance.ossosColetados >= instance.objetivoDeOssos)
            {
                instance.FinalizarJogo(true);
            }
        }
    }

    private void AtualizarUI()
    {
        if (scoreText != null)
            scoreText.text = "Ossos: " + ossosColetados + " / " + objetivoDeOssos;
    }

    private void FinalizarJogo(bool vitoria)
    {
        isGameOver = true;
        Time.timeScale = 0; // Pausa o jogo para o menu aparecer

        if (vitoria && win != null) 
            win.SetActive(true);
        else if (gameOver != null) 
            gameOver.SetActive(true);
    }

    public void TentarNovamente()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarAoMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Menu"); // Verifique se o nome é "Menu" ou "MainMenu"
    }
}
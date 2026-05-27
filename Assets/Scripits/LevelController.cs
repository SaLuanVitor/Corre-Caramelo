using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour 
{
    public static LevelController instance; 
    
    [Header("Configurações de UI")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text levelText; 
    public GameObject win;       
    public GameObject gameOver;  

    [Header("Configurações do Jogo")]
    public float tempoTotal = 60f;

    private float timer;
    private int ossosColetados;
    private int objetivoAtual;
    private int nivelAtual;
    private bool isGameOver;

    private void Awake()
    {
        instance = this;
        // Carrega o nível salvo (se for a primeira vez, inicia no 1)
        nivelAtual = PlayerPrefs.GetInt("NivelAtual", 1);
        
        // Regra: Fase 1 = 5, Fase 2 = 10, Fase 3 = 15...
        objetivoAtual = nivelAtual * 5;
        
        timer = tempoTotal;
        Time.timeScale = 1; 
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
        else FinalizarJogo(false);
    }

    public static void ColetarOsso()
    {
        if (instance != null && !instance.isGameOver)
        {
            instance.ossosColetados++;
            instance.AtualizarUI();
            if (instance.ossosColetados >= instance.objetivoAtual)
                instance.FinalizarJogo(true);
        }
    }

    public static void Morreu() => instance?.FinalizarJogo(false);

    private void AtualizarUI()
    {
        if (scoreText != null) scoreText.text = "Ossos: " + ossosColetados + " / " + objetivoAtual;
        if (levelText != null) levelText.text = "Fase: " + nivelAtual;
    }

    private void FinalizarJogo(bool vitoria)
    {
        isGameOver = true;
        Time.timeScale = 0; 
        if (vitoria && win != null) win.SetActive(true);
        else if (gameOver != null) gameOver.SetActive(true);
    }

    // --- FUNÇÕES DOS BOTÕES ---
    public void ProximaFase() {
        // Aumenta o nível na memória antes de recarregar
        PlayerPrefs.SetInt("NivelAtual", nivelAtual + 1);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TentarNovamente() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarAoMenu() {
        PlayerPrefs.SetInt("NivelAtual", 1); // Reseta a dificuldade ao sair
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
// Nome do arquivo: LevelController.cs
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

    [Header("Configurações de Áudio")]
    public AudioClip somVitoria;
    public AudioClip somDerrota;
    [Range(0f, 1f)]
    public float volumeDosSons = 0.8f; // Barrinha de volume no Unity

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
    }

    private void Start()
    {
        // Garante a leitura correta do progresso das fases
        nivelAtual = PlayerPrefs.GetInt("NivelAtual", 1);
        
        // Regra exata: Fase 1 = 5, Fase 2 = 10, Fase 3 = 15...
        objetivoAtual = nivelAtual * 5;
        
        ossosColetados = 0;
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
        
        if (vitoria) 
        {
            // Toca o som de vitória se ele existir
            if (somVitoria != null)
                AudioSource.PlayClipAtPoint(somVitoria, Camera.main.transform.position, volumeDosSons);
                
            if (win != null) win.SetActive(true);
        }
        else 
        {
            // Toca o som de derrota se ele existir
            if (somDerrota != null)
                AudioSource.PlayClipAtPoint(somDerrota, Camera.main.transform.position, volumeDosSons);
                
            if (gameOver != null) gameOver.SetActive(true);
        }
    }

    public void ProximaFase() {
        PlayerPrefs.SetInt("NivelAtual", nivelAtual + 1);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TentarNovamente() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarAoMenu() {
        PlayerPrefs.SetInt("NivelAtual", 1); 
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
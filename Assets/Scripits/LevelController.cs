using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using UnityEngine.EventSystems;

public class LevelController : MonoBehaviour 
{
    public static LevelController instance; 
    
    [Header("Configurações de UI")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text levelText; 
    public GameObject win;       
    public GameObject gameOver;  

    [Header("Configurações de Controle")]
    public GameObject botaoProximaFase;
    public GameObject botaoReiniciar;   

    [Header("Configurações de QR Code")]
    public RawImage telaDoQRCode; 
    public Texture2D[] listaDeQRCodes; 

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
        nivelAtual = PlayerPrefs.GetInt("NivelAtual", 1);
        objetivoAtual = nivelAtual * 5;
        ossosColetados = 0;
        timer = tempoTotal;
        isGameOver = false;
        Time.timeScale = 1; 
        AtualizarUI();
        
        // Garante que o Caramelo pode andar livremente no início
        EventSystem.current.SetSelectedGameObject(null);
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
            FinalizarJogo(false); // O tempo acabou, o jogador morre
        }
    }

    public static void ColetarOsso()
    {
        if (instance != null && !instance.isGameOver)
        {
            instance.ossosColetados++;
            instance.AtualizarUI();
            
            // Verifica se bateu a meta para ganhar a fase
            if (instance.ossosColetados >= instance.objetivoAtual)
                instance.FinalizarJogo(true);
        }
    }

    public static void Morreu() 
    {
        if (instance != null && !instance.isGameOver) 
            instance.FinalizarJogo(false);
    }

    private void AtualizarUI()
    {
        if (scoreText != null) scoreText.text = "Ossos: " + ossosColetados + " / " + objetivoAtual;
        if (levelText != null) levelText.text = "Fase: " + nivelAtual;
    }

    public void FinalizarJogo(bool vitoria)
    {
        isGameOver = true;
        Time.timeScale = 0; 
        
        // Limpa as seleções para o controle focar nos botões novos
        EventSystem.current.SetSelectedGameObject(null); 
        
        if (vitoria) 
        {
            // Lógica do QR Code
            if (telaDoQRCode != null && listaDeQRCodes != null && listaDeQRCodes.Length > 0)
            {
                int indice = nivelAtual - 1; 
                if (indice < listaDeQRCodes.Length && listaDeQRCodes[indice] != null)
                {
                    telaDoQRCode.texture = listaDeQRCodes[indice];
                    telaDoQRCode.gameObject.SetActive(true);
                }
                else telaDoQRCode.gameObject.SetActive(false);
            }

            if (win != null) win.SetActive(true);
            if (botaoProximaFase != null) EventSystem.current.SetSelectedGameObject(botaoProximaFase);
        }
        else 
        {
            if (gameOver != null) gameOver.SetActive(true);
            if (botaoReiniciar != null) EventSystem.current.SetSelectedGameObject(botaoReiniciar);
        }
    }

    public void ProximaFase() { Time.timeScale = 1; SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void TentarNovamente() { Time.timeScale = 1; SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void VoltarAoMenu() { Time.timeScale = 1; SceneManager.LoadScene("Menu"); }
}
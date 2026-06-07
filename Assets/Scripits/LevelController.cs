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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1; 
    }

    public static void ColetarOsso()
    {
        if (instance != null) instance.FinalizarJogo(true);
    }

    public static void Morreu() 
    {
        if (instance != null) instance.FinalizarJogo(false);
    }

    public void FinalizarJogo(bool vitoria)
    {
        Time.timeScale = 0; 
        EventSystem.current.SetSelectedGameObject(null); 
        
        if (vitoria) 
        {
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
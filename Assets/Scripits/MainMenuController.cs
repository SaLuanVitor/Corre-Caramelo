using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections; // Necessário para a espera (Coroutine)

public class MainMenuController : MonoBehaviour
{
    [Header("Configuração de Controle")]
    public GameObject botaoJogar; 

    private void Start()
    {
        // Inicia a rotina que espera a tela carregar
        StartCoroutine(SelecionarBotao());
    }

    private IEnumerator SelecionarBotao()
    {
        // Espera um frame inteiro passar para o Unity não bugar o controle
        yield return null; 
        
        EventSystem.current.SetSelectedGameObject(null);
        if (botaoJogar != null) 
        {
            EventSystem.current.SetSelectedGameObject(botaoJogar);
        }
    }

    public void Jogar()
    {
        PlayerPrefs.SetInt("NivelAtual", 1); 
        SceneManager.LoadScene("Level 1");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
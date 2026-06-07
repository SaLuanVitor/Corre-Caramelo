using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Jogar()
    {
        // Garante que o jogo começa do zero
        PlayerPrefs.SetInt("NivelAtual", 1); 
        SceneManager.LoadScene("Level 1");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void IniciarJogo()
    {
        // Mudei de "Coleta" para "Menu" (ou o nome exato do arquivo na sua pasta Scenes)
        SceneManager.LoadScene("Level 1"); 
    }
}
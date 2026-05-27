using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public void ProximaFase()
    {
        // MUITO IMPORTANTE: Descongela o jogo antes de carregar a nova fase
        Time.timeScale = 1; 
        SceneManager.LoadScene("Level 1"); 
    }

    public void Sair()
    {
        // Descongela o tempo para que o Menu funcione corretamente
        Time.timeScale = 1; 
        SceneManager.LoadScene("Menu"); 
    }
}
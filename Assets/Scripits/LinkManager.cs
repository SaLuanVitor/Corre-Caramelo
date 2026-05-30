using UnityEngine;

public class LinkManager : MonoBehaviour
{
    // A palavra "public" é essencial para o botão enxergar essa função
    public void AbrirInstagram()
    {
        // Coloque o link completo aqui dentro das aspas, incluindo o https://
        Application.OpenURL("https://www.instagram.com/ardap.ong/");
    }
}
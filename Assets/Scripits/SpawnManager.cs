// Nome do arquivo: SpawnManager.cs
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject petiscoPrefab; 
    public float distanciaMinima = 2.0f; // Ajuste no Inspector se precisar de mais espaço
    public float xMin = -15f, xMax = 15f, yMin = -8f, yMax = 8f;

    void Start()
    {
        if (petiscoPrefab == null) return;

        int nivel = PlayerPrefs.GetInt("NivelAtual", 1);
        int totalParaCriar = nivel * 5; 

        for (int i = 0; i < totalParaCriar; i++)
        {
            TentarSpawnar();
        }
    }

    void TentarSpawnar()
    {
        for (int i = 0; i < 100; i++) 
        {
            Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
            
            // Analisa o espaço ao redor ignorando colisores que não sejam petiscos
            Collider2D[] colisoresProximos = Physics2D.OverlapCircleAll(pos, distanciaMinima);
            bool espacoLivre = true;

            foreach (Collider2D colisor in colisoresProximos)
            {
                if (colisor.CompareTag("Petisco"))
                {
                    espacoLivre = false;
                    break; 
                }
            }

            if (espacoLivre)
            {
                Instantiate(petiscoPrefab, pos, Quaternion.identity);
                return; 
            }
        }
        
        // Se o mapa estiver muito cheio, instancia mesmo assim para não faltar itens no objetivo
        Instantiate(petiscoPrefab, new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
    }
}
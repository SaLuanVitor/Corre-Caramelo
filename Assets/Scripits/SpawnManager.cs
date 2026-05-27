using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject petiscoPrefab; 
    public float distanciaMinima = 2.5f;
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
            if (Physics2D.OverlapCircle(pos, distanciaMinima) == null)
            {
                Instantiate(petiscoPrefab, pos, Quaternion.identity);
                return; 
            }
        }
        // Fallback: se não achar espaço, cria em um lugar aleatório para não faltar osso
        Instantiate(petiscoPrefab, new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
    }
}
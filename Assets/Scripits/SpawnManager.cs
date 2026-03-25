using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject petiscoPrefab; 
    public int maxPetiscos = 10;     // O limite que você quer
    public int quantidadeInicial = 10; 
    public float tempoEspera = 2f;   

    [Header("Limites do Cenário")]
    public float xMin = -8f;
    public float xMax = 8f;
    public float yMin = -4f;
    public float yMax = 4f;

    void Start()
    {
        // Enche o cenário com 10 petiscos no começo
        for (int i = 0; i < quantidadeInicial; i++)
        {
            GerarObjeto();
        }
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // CONTAGEM CRÍTICA: Só funciona se o Prefab tiver a Tag "Petisco"
            int contagem = GameObject.FindGameObjectsWithTag("Petisco").Length;

            if (contagem < maxPetiscos)
            {
                GerarObjeto();
            }
            yield return new WaitForSeconds(tempoEspera);
        }
    }

    void GerarObjeto()
    {
        if (petiscoPrefab == null) return;

        Vector3 posicaoAleatoria = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        Instantiate(petiscoPrefab, posicaoAleatoria, Quaternion.identity);
    }
}
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject inimigoPrefab; // Arraste o Prefab Mlk1 aqui
    public Transform[] pontosDeSpawn; // Arraste objetos vazios da cena aqui

    void Start()
    {
        if (inimigoPrefab == null || pontosDeSpawn.Length == 0) return;

        int nivel = PlayerPrefs.GetInt("NivelAtual", 1);
        
        // Matemática: Nível 1 e 2 = 1 inimigo | Nível 3 e 4 = 2 inimigos
        int quantidadeInimigos = (nivel + 1) / 2;

        for (int i = 0; i < quantidadeInimigos; i++)
        {
            int indice = Random.Range(0, pontosDeSpawn.Length);
            Instantiate(inimigoPrefab, pontosDeSpawn[indice].position, Quaternion.identity);
        }
    }
}